using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OsuSharp.Entities;
using OsuSharp.Exceptions;
using OsuSharp.Extensions;
using OsuSharp.Logging;
using OsuSharp.Net.Serialization;

namespace OsuSharp.Net
{
    internal sealed class RequestHandler : IDisposable
    {
        private readonly OsuClient _client;
        private readonly HttpClient _httpClient;
        private readonly ConcurrentDictionary<string, RatelimitBucket> _ratelimits;
        private readonly DefaultJsonSerializer _serializer;
        private bool _disposed;

        internal RequestHandler(
            OsuClient client)
        {
            _client = client;
            _ratelimits = new ConcurrentDictionary<string, RatelimitBucket>();
            _serializer = DefaultJsonSerializer.Instance;
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

        internal void UpdateAuthorizationHeader(
            OsuToken token)
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(token.Type.ToString(), token.AccessToken);
        }

        private async Task<RatelimitBucket> GetBucketFromEndpointAsync(
            string endpoint)
        {
            _client.Configuration.Logger.Log(LogLevel.Debug, EventIds.RateLimits,
                "Retrieving rate-limit bucket for [{Endpoint}]", endpoint);

            if (_ratelimits.TryGetValue(endpoint, out var bucket) && !bucket.HasExpired && bucket.Remaining <= 0)
            {
                _client.Configuration.Logger.Log(LogLevel.Warning, EventIds.RateLimits,
                    "Pre-emptive rate-limit bucket hit for [{Endpoint}]: [{Amount}/{Limit}]",
                    endpoint, bucket.Limit - bucket.Remaining, bucket.Limit);

                if (!_client.Configuration.ThrowOnRateLimits)
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

            _client.Configuration.Logger.Log(LogLevel.Debug, EventIds.RateLimits,
                "Rate-limit bucket passed for [{Endpoint}]: [{Amount}/{Limit}]",
                endpoint, bucket.Limit - bucket.Remaining, bucket.Limit);
        }

        internal async Task SendAsync(
            OsuApiRequest request)
        {
            var (bucket, requestMessage) = await PrepareRequestAsync(request).ConfigureAwait(false);
            var response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);
            await ValidateResponseAsync(response).ConfigureAwait(false);
            UpdateBucket(request.Endpoint, bucket, response);
        }

        internal async Task<T> SendAsync<T>(
            OsuApiRequest request)
            where T : class
        {
            var (bucket, requestMessage) = await PrepareRequestAsync(request).ConfigureAwait(false);
            var response = await _httpClient.SendAsync(requestMessage).ConfigureAwait(false);
            await ValidateResponseAsync(response).ConfigureAwait(false);
            return await ReadAndDeserializeAsync<T>(request, response, bucket).ConfigureAwait(false);
        }

        private async Task<(RatelimitBucket, HttpRequestMessage)> PrepareRequestAsync(
            OsuApiRequest request)
        {
            request.Parameters ??= new Dictionary<string, string>();

            var paramsString = request.Parameters.ToLogString();
            _client.Configuration.Logger.Log(LogLevel.Information, EventIds.RestApi,
                "Getting [{LocalPath}] with parameters [{Params}]",
                request.Route.ToString(), paramsString);

            var bucket = await GetBucketFromEndpointAsync(request.Endpoint).ConfigureAwait(false);

            var requestMessage = new HttpRequestMessage();
            if (request.Method == HttpMethod.Get && request.Parameters is {Count: > 0})
            {
                request.Route = new Uri(request.Route, request.Parameters.AsQueryString());
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
            OsuApiRequest request,
            HttpResponseMessage response,
            RatelimitBucket bucket)
            where T : class
        {
            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var streamReader = new StreamReader(stream);
            var content = await streamReader.ReadToEndAsync().ConfigureAwait(false);

            _client.Configuration.Logger.Log(LogLevel.Trace, EventIds.RestApi, "Response received: {Content}", content);

            UpdateBucket(request.Endpoint, bucket, response);
            return _serializer.Deserialize<T>(stream);
        }
    }
}