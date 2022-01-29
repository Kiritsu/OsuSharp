using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OsuSharp.Exceptions;
using OsuSharp.Extensions;
using OsuSharp.JsonModels;
using OsuSharp.Models;
using OsuSharp.Net.Serialization;
using OsuSharp.Interfaces;
using OsuSharp.Mapper;
using System.Threading;
using System.IO;

namespace OsuSharp.Net
{
    internal sealed class DefaultRequestHandler : IRequestHandler
    {
        private readonly ILogger<DefaultRequestHandler> _logger;
        private readonly IOsuClientConfiguration _configuration;
        private readonly IJsonSerializer _serializer;

        private readonly HttpClient _httpClient;
        private readonly ConcurrentDictionary<string, RatelimitBucket> _ratelimits;

        private bool _disposed;

        public DefaultRequestHandler(
            ILogger<DefaultRequestHandler> logger,
            IOsuClientConfiguration configuration,
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
                BaseAddress = new Uri(_configuration.BaseUrl)
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
            IOsuApiRequest request,
            CancellationToken token = default)
        {
            _httpClient.DefaultRequestHeaders.Authorization = request.Token is not null
                ? new AuthenticationHeaderValue(request.Token.Type.ToString(), request.Token.AccessToken)
                : null;

            var (bucket, requestMessage) = await PrepareRequestAsync(request, token).ConfigureAwait(false);
            var response = await _httpClient.SendAsync(requestMessage, token).ConfigureAwait(false);
            await ValidateResponseAsync(response, token).ConfigureAwait(false);
            UpdateBucket(request.Endpoint, bucket, response);
        }

        public async Task<Stream> GetStreamAsync(
            IOsuApiRequest request,
            CancellationToken token = default)
        {
            _httpClient.DefaultRequestHeaders.Authorization = request.Token is not null
                ? new AuthenticationHeaderValue(request.Token.Type.ToString(), request.Token.AccessToken)
                : null;

            var (bucket, requestMessage) = await PrepareRequestAsync(request, token).ConfigureAwait(false);
            var response = await _httpClient.SendAsync(requestMessage, token).ConfigureAwait(false);
            await ValidateResponseAsync(response, token).ConfigureAwait(false);
            UpdateBucket(request.Endpoint, bucket, response);

            return await response.Content.ReadAsStreamAsync(token).ConfigureAwait(false);
        }

        public async Task<T> SendAsync<T>(
            IOsuApiRequest request,
            CancellationToken token = default)
            where T : class
        {
            _httpClient.DefaultRequestHeaders.Authorization = request.Token is not null 
                ? new AuthenticationHeaderValue(request.Token.Type.ToString(), request.Token.AccessToken) 
                : null;

            var (bucket, requestMessage) = await PrepareRequestAsync(request, token).ConfigureAwait(false);
            var response = await _httpClient.SendAsync(requestMessage, token).ConfigureAwait(false);
            await ValidateResponseAsync(response, token).ConfigureAwait(false);
            UpdateBucket(request.Endpoint, bucket, response);
            return await ReadAndDeserializeAsync<T>(request, response, bucket, token).ConfigureAwait(false);
        }

        public async Task<TImplementation> SendAsync<TImplementation, TModel>(
            IOsuApiRequest request,
            CancellationToken token = default)
            where TModel : class
        {
            var model = await SendAsync<TModel>(request, token).ConfigureAwait(false);
            return OsuSharpMapper.Transform<TImplementation, TModel>(model, request.Client);
        }

        public async Task<IReadOnlyList<TImplementation>> SendMultipleAsync<TImplementation, TModel>(
            IOsuApiRequest request, 
            CancellationToken token = default) 
            where TModel : class
        {
            var model = await SendAsync<List<TModel>>(request, token).ConfigureAwait(false);
            return model
                .Select(x => OsuSharpMapper.Transform<TImplementation, TModel>(x, request.Client))
                .ToList()
                .AsReadOnly();
        }

        private async Task<RatelimitBucket> GetBucketFromEndpointAsync(
            string endpoint,
            CancellationToken token = default)
        {
            _logger.Log(LogLevel.Debug,
                "Retrieving rate-limit bucket for [{Endpoint}]", endpoint);

            if (_ratelimits.TryGetValue(endpoint, out var bucket) && !bucket.HasExpired && bucket.Remaining <= 0)
            {
                _logger.Log(LogLevel.Warning,
                    "Pre-emptive rate-limit bucket hit for [{Endpoint}]: [{Amount}/{Limit}]",
                    endpoint, bucket.Limit - bucket.Remaining, bucket.Limit);

                if (!_configuration.ThrowOnRateLimits)
                {
                    await Task.Delay(bucket.ExpiresIn, token).ConfigureAwait(false);
                }
                else
                {
                    throw new PreemptiveRateLimitException(bucket.ExpiresIn);
                }
            }
            else if (bucket == null)
            {
                bucket = new RatelimitBucket();
                _ratelimits.TryRemove(endpoint, out _);
                _ratelimits.TryAdd(endpoint, bucket);
            }

            return bucket;
        }

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

            _logger.Log(LogLevel.Debug,
                "Rate-limit bucket passed for [{Endpoint}]: [{Amount}/{Limit}] (Expires In {Timespan})",
                endpoint, bucket.Limit - bucket.Remaining, bucket.Limit, bucket.ExpiresIn.ToString("g"));
        }

        private async Task<(RatelimitBucket, HttpRequestMessage)> PrepareRequestAsync(
            IOsuApiRequest request, 
            CancellationToken token = default)
        {
            request.Parameters ??= new Dictionary<string, string>();

            var paramsString = request.Parameters.AsLogString();
            _logger.Log(LogLevel.Information,
                "Getting [{LocalPath}] with parameters [{Params}]",
                request.Route.ToString(), paramsString);

            var bucket = await GetBucketFromEndpointAsync(request.Endpoint, token).ConfigureAwait(false);

            var requestMessage = new HttpRequestMessage();
            if (request.Method == HttpMethod.Get && request.Parameters is { Count: > 0 })
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
            HttpResponseMessage response,
            CancellationToken token = default)
        {
            if (!response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync(token).ConfigureAwait(false);
                throw new ApiException(response.ReasonPhrase, response.StatusCode, jsonResponse);
            }
        }

        private async Task<T> ReadAndDeserializeAsync<T>(
            IOsuApiRequest request,
            HttpResponseMessage response,
            RatelimitBucket bucket,
            CancellationToken token = default)
            where T : class
        {
            var bytes = await response.Content.ReadAsByteArrayAsync(token).ConfigureAwait(false);
            var content = Encoding.UTF8.GetString(bytes);

            _logger.Log(LogLevel.Trace, "Response received: {Content}", content);

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

        private void LogMissingFields<T>(T model, string name = "")
        {
            if (model is JsonModel { ExtensionData.Count: > 0 } jsonModel && jsonModel.GetType() != typeof(JsonModel))
            {
                _logger.Log(LogLevel.Trace,
                    "Found {Count} extra fields for model {Model} - {Name}:\n{Data}",
                    jsonModel.ExtensionData.Count, model.GetType().Name, name, string.Join("\n", jsonModel.ExtensionData));

                foreach (var property in model.GetType().GetProperties())
                {
                    var value = property.GetValue(model);

                    if (property.GetValue(model) is JsonModel { ExtensionData.Count: > 0 } && jsonModel.GetType() != typeof(JsonModel))
                    {
                        LogMissingFields(value, property.Name);
                    }
                }
            }
        }
    }
}