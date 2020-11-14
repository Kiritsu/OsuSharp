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

        internal RequestHandler(OsuClient client)
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
            });
            _httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("OsuSharp", "2.0"));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        internal void UpdateAuthorizationHeader(OsuToken token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue(token.Type.ToString(), token.AccessToken);
        }
        
        internal async Task<RatelimitBucket> GetBucketFromUriAsync(Uri uri)
        {
            _client.Configuration.Logger.Log(LogLevel.Debug, EventIds.RateLimits,
                $"Retrieving rate-limit bucket for [{uri.LocalPath}]");

            if (_ratelimits.TryGetValue(uri.LocalPath, out var bucket) && !bucket.HasExpired && bucket.Remaining <= 0)
            {
                _client.Configuration.Logger.Log(LogLevel.Warning, EventIds.RateLimits,
                    $"Pre-emptive rate-limit bucket hit for [{uri.LocalPath}]: [{bucket.Limit - bucket.Remaining}/{bucket.Limit}]");

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
                _ratelimits.TryAdd(uri.LocalPath, bucket);
            }

            return bucket;
        }

        // todo: to be reworked when rate limits are here! cf: ##6839
        internal void UpdateBucket(Uri uri, RatelimitBucket bucket, HttpResponseMessage response)
        {
            bucket.Limit =
                response.Headers.TryGetValues("X-RateLimit-Limit", out var limitHeaders)
                    ? int.Parse(limitHeaders.First())
                    : 1200;

            bucket.Remaining =
                response.Headers.TryGetValues("X-RateLimit-Remaining", out var remainingHeaders)
                    ? int.Parse(remainingHeaders.First())
                    : 1200;

            if (bucket.HasExpired)
            {
                bucket.CreatedAt = DateTimeOffset.Now;
            }
            else
            {
                bucket.Remaining--;
            }

            _client.Configuration.Logger.Log(LogLevel.Debug, EventIds.RateLimits,
                $"Rate-limit bucket passed for [{uri.LocalPath}]: [{bucket.Limit - bucket.Remaining}/{bucket.Limit}]");
        }

        internal async Task<T> GetAsync<T>(Uri route, IReadOnlyDictionary<string, string> parameters = null)
            where T : class
        {
            parameters ??= new Dictionary<string, string>();

            var paramsString = string.Join(" | ", parameters.Select(x => $"{x.Key}:{x.Value}"));
            _client.Configuration.Logger.Log(LogLevel.Information, EventIds.RestApi,
                $"Getting [{route.LocalPath}] with parameters [{(string.IsNullOrWhiteSpace(paramsString) ? "no_params" : paramsString)}]");

            if (parameters is { Count: > 0 })
            {
                route = new Uri(route, $"?{string.Join("&", parameters.Select(x => $"{x.Key}={x.Value}"))}");
            }

            var bucket = await GetBucketFromUriAsync(route).ConfigureAwait(false);
            var response = await _httpClient.GetAsync(route).ConfigureAwait(false);

            return await HandleResponseAsync<T>(route, response, bucket).ConfigureAwait(false);
        }

        internal async Task<T> PostAsync<T>(Uri route, IReadOnlyDictionary<string, string> parameters) where T : class
        {
            var paramsString = string.Join(" | ", parameters.Select(x => $"{x.Key}:{x.Value}"));
            _client.Configuration.Logger.Log(LogLevel.Information, EventIds.RestApi,
                $"Posting [{route.LocalPath}] with parameters [{paramsString}]");

            var bucket = await GetBucketFromUriAsync(route).ConfigureAwait(false);
            var response = await _httpClient.PostAsync(route, new FormUrlEncodedContent(parameters))
                .ConfigureAwait(false);

            return await HandleResponseAsync<T>(route, response, bucket).ConfigureAwait(false);
        }

        internal async Task<T> HandleResponseAsync<T>(Uri route, HttpResponseMessage response, RatelimitBucket bucket)
            where T : class
        {
            if (!response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                throw new ApiException(response.ReasonPhrase, response.StatusCode, jsonResponse);
            }

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var streamReader = new StreamReader(stream);
            var content = await streamReader.ReadToEndAsync().ConfigureAwait(false);

            _client.Configuration.Logger.Log(LogLevel.Trace, EventIds.RestApi, $"Response received: {content}");

            UpdateBucket(route, bucket, response);
            return _serializer.Deserialize<T>(stream);
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
    }
}