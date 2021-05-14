using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Mapster;
using Microsoft.Extensions.Logging;
using OsuSharp.Domain;
using OsuSharp.Exceptions;
using OsuSharp.Extensions;
using OsuSharp.JsonModels;
using OsuSharp.Logging;
using OsuSharp.Models;
using OsuSharp.Net.Serialization;
using OsuSharp.Interfaces;

namespace OsuSharp.Net
{
    internal sealed class DefaultRequestHandler : IRequestHandler
    {
        private readonly ILogger<DefaultRequestHandler> _logger;
        private readonly OsuClientConfiguration _configuration;
        private readonly IJsonSerializer _serializer;

        private readonly HttpClient _httpClient;
        private readonly ConcurrentDictionary<string, RatelimitBucket> _ratelimits;

        private bool _disposed;

        private static readonly Assembly DomainAssembly = Assembly.GetAssembly(typeof(User));
        private static readonly Dictionary<Type, Type> MappedTypes = 
            Assembly.GetAssembly(typeof(JsonModel))!
                .GetTypes()
                .Where(x => x.IsAssignableTo(typeof(JsonModel)) && x != typeof(JsonModel) && !x.IsAbstract)
                .Select(x => new {JsonModel = x, Model = DomainAssembly.ExportedTypes.FirstOrDefault(y => y.Name == x.Name[..^9])})
                .ToDictionary(x => x.JsonModel, x => x.Model!);

        static DefaultRequestHandler()
        {
            foreach (var (jsonModel, model) in MappedTypes)
            {
                var ctor = model
                    .GetConstructor(BindingFlags.NonPublic | BindingFlags.Instance, 
                        null, CallingConventions.Any, Array.Empty<Type>(), null);

                var adapterConfigType = typeof(TypeAdapterConfig<,>)
                    .MakeGenericType(jsonModel, model);

                var method = adapterConfigType
                    .GetMethod("NewConfig", BindingFlags.Static | BindingFlags.Public);

                var result = method!.Invoke(null, null);
                var resultObjectType = result!
                    .GetType();
                
                method = resultObjectType
                    .GetMethod("MapToConstructor", BindingFlags.Public | BindingFlags.Instance);

                method!.Invoke(result, new object[]{ctor});
            }
        }

        public DefaultRequestHandler(
            ILogger<DefaultRequestHandler> logger,
            OsuClientConfiguration configuration,
            IJsonSerializer serializer)
        {
            _logger = logger;
            _configuration = configuration;
            _serializer = serializer ?? DefaultJsonSerializer.Instance;

            _ratelimits = new ConcurrentDictionary<string, RatelimitBucket>();
            _httpClient = new HttpClient(new RedirectHandler
            {
                InnerHandler = new HttpClientHandler
                {
                    AllowAutoRedirect = false,
                    AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                }
            })
            {
                BaseAddress = new Uri("https://osu.ppy.sh")
            };

            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("OsuSharp", "2.0"));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
            _httpClient.Dispose();
        }

        public async Task SendAsync(
            IOsuApiRequest request)
        {
            if (request.Token is not null)
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(request.Token.Type.ToString(), request.Token.AccessToken);
            }

            var (bucket, requestMessage) = await PrepareRequestAsync(request).ConfigureAwait(false);
            var response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);
            await ValidateResponseAsync(response).ConfigureAwait(false);
            UpdateBucket(request.Endpoint, bucket, response);
        }

        public async Task<T> SendAsync<T>(
            IOsuApiRequest request)
            where T : class
        {
            if (request.Token is not null)
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue(request.Token.Type.ToString(), request.Token.AccessToken);
            }

            var (bucket, requestMessage) = await PrepareRequestAsync(request).ConfigureAwait(false);
            var response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);
            await ValidateResponseAsync(response).ConfigureAwait(false);
            return await ReadAndDeserializeAsync<T>(request, response, bucket).ConfigureAwait(false);
        }
        
        public async Task<T> SendAsync<T, TModel>(
            IOsuApiRequest request)
            where T : class
            where TModel : class
        {
            var jsonModel = await SendAsync<TModel>(request);
            return jsonModel.Adapt<T>();
        }

        private async Task<RatelimitBucket> GetBucketFromEndpointAsync(
            string endpoint)
        {
            _logger.Log(LogLevel.Debug, EventIds.RateLimits,
                "Retrieving rate-limit bucket for [{Endpoint}]", endpoint);

            if (_ratelimits.TryGetValue(endpoint, out var bucket) && !bucket.HasExpired && bucket.Remaining <= 0)
            {
                _logger.Log(LogLevel.Warning, EventIds.RateLimits,
                    "Pre-emptive rate-limit bucket hit for [{Endpoint}]: [{Amount}/{Limit}]",
                    endpoint, bucket.Limit - bucket.Remaining, bucket.Limit);

                if (!_configuration.ThrowOnRateLimits)
                {
                    await Task.Delay(bucket.ExpiresIn).ConfigureAwait(false);
                }
                else
                {
                    throw new PreemptiveRateLimitException
                    {
                        ExpiresIn = bucket.ExpiresIn
                    };
                }
            }
            else
            {
                bucket = new RatelimitBucket();
                _ratelimits.TryAdd(endpoint, bucket);
            }

            return bucket;
        }

        // todo: to be reworked when rate limits are here! cf: #ppy/osu-web#6839
        private void UpdateBucket(
            string endpoint,
            RatelimitBucket bucket,
            HttpResponseMessage response)
        {
            bucket.Limit =
                response.Headers.TryGetValues("X-RateLimit-Limit", out var limitHeaders)
                    ? int.Parse(limitHeaders.First())
                    : 1200;

            bucket.Remaining =
                response.Headers.TryGetValues("X-RateLimit-Remaining", out var remainingHeaders)
                    ? int.Parse(remainingHeaders.First())
                    : bucket.Remaining - 1;

            if (bucket.HasExpired)
            {
                bucket.CreatedAt = DateTimeOffset.Now;
            }

            _logger.Log(LogLevel.Debug, EventIds.RateLimits,
                "Rate-limit bucket passed for [{Endpoint}]: [{Amount}/{Limit}]",
                endpoint, bucket.Limit - bucket.Remaining, bucket.Limit);
        }

        private async Task<(RatelimitBucket, HttpRequestMessage)> PrepareRequestAsync(
            IOsuApiRequest request)
        {
            request.Parameters ??= new Dictionary<string, string>();

            var paramsString = request.Parameters.AsLogString();
            _logger.Log(LogLevel.Information, EventIds.RestApi,
                "Getting [{LocalPath}] with parameters [{Params}]",
                request.Route.ToString(), paramsString);

            var bucket = await GetBucketFromEndpointAsync(request.Endpoint).ConfigureAwait(false);

            var requestMessage = new HttpRequestMessage();
            if (request.Method == HttpMethod.Get && request.Parameters is {Count: > 0})
            {
                var url = request.Route + request.Parameters.AsQueryString();
                request.Route = new Uri(url, UriKind.Relative);
            }
            else
            {
                requestMessage.Content = new FormUrlEncodedContent(request.Parameters);
            }

            requestMessage.Method = request.Method;
            requestMessage.RequestUri = request.Route;

            return (bucket, requestMessage);
        }

        private static async Task ValidateResponseAsync(
            HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new ApiException(response.ReasonPhrase, response.StatusCode, jsonResponse);
            }
        }

        private async Task<T> ReadAndDeserializeAsync<T>(
            IOsuApiRequest request,
            HttpResponseMessage response,
            RatelimitBucket bucket)
            where T : class
        {
            var bytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
            var content = Encoding.UTF8.GetString(bytes);

            _logger.Log(LogLevel.Trace, EventIds.RestApi, "Response received: {Content}", content);

            UpdateBucket(request.Endpoint, bucket, response);
            var model = _serializer.Deserialize<T>(content);

            if (model is IEnumerable enumerable)
            {
                foreach (var subModel in enumerable)
                {
                    LogMissingFields(subModel);
                }
            }
            else
            {
                LogMissingFields(model);
            }

            return model;
        }

        private void LogMissingFields<T>(T model)
        {
            if (model is JsonModel {ExtensionData: {Count: > 0}} jsonModel && jsonModel.GetType() != typeof(JsonModel))
            {
                _logger.Log(LogLevel.Debug, EventIds.Deserialization,
                    "Found {Count} extra fields for model {Model}:\n{Data}",
                    jsonModel.ExtensionData.Count, typeof(T).Name, string.Join('\n', jsonModel.ExtensionData));
            }
        }
    }
}