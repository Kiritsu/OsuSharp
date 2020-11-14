using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OsuSharp.Entities;
using OsuSharp.Enums;
using OsuSharp.Exceptions;
using OsuSharp.Logging;
using OsuSharp.Serialization;

namespace OsuSharp
{
    public sealed class OsuClient : IDisposable
    {
        internal readonly OsuClientConfiguration Configuration;
        internal readonly HttpClient HttpClient;
        internal readonly ConcurrentDictionary<string, RatelimitBucket> Ratelimits;
        internal readonly DefaultJsonSerializer Serializer;

        private bool _disposed;
        private OsuToken _credentials;

        /// <summary>
        ///     Gets the current used credentials to communicate with the API.
        /// </summary>
        public OsuToken Credentials
        {
            get => _credentials;
            internal set
            {
                _credentials = value;
                HttpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", _credentials.ToString());
            }
        }

        /// <summary>
        ///     Initializes a new OsuClient with the given configuration.
        /// </summary>
        /// <param name="configuration">
        ///     Configuration of the client.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <see cref="configuration"/> is null
        /// </exception>
        public OsuClient([NotNull] OsuClientConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Configuration.Logger ??= new DefaultLogger(Configuration);
            Ratelimits = new ConcurrentDictionary<string, RatelimitBucket>();
            Serializer = DefaultJsonSerializer.Instance;

            HttpClient = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            });
            HttpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("OsuSharp", "2.0"));
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        ///     Gets or requests an API access token.
        /// </summary>
        /// <returns>
        ///     Returns an <see cref="OsuToken"/>.
        /// </returns>
        public async ValueTask<OsuToken> GetOrUpdateAccessTokenAsync()
        {
            ThrowIfDisposed();

            // todo: handle refresh token
            if (Credentials != null && !Credentials.HasExpired)
            {
                return Credentials;
            }

            var parameters = new Dictionary<string, string>
            {
                ["client_id"] = Configuration.ClientId.ToString(),
                ["client_secret"] = Configuration.ClientSecret,
                ["grant_type"] = "client_credentials",
                ["scope"] = "public"
            };

            Uri.TryCreate($"{Endpoints.Domain}{Endpoints.Oauth}{Endpoints.Token}", UriKind.Absolute, out var uri);
            var response = await PostAsync<AccessTokenResponse>(uri, parameters).ConfigureAwait(false);

            return Credentials = new OsuToken
            {
                Type = Enum.Parse<TokenType>(response.TokenType),
                AccessToken = response.AccessToken,
                ExpiresInSeconds = response.ExpiresIn
            };
        }

        public async Task<UserCompact> GetUserAsync(string username)
        {
            Uri.TryCreate($"{Endpoints.Domain}{Endpoints.Api}{Endpoints.Users}/{username}/osu", UriKind.Absolute, out var uri);
            var response = await GetAsync<UserCompact>(uri);
            return response;
        }

        internal async Task<RatelimitBucket> GetBucketFromUriAsync(Uri uri)
        {
            Configuration.Logger.Log(LogLevel.Debug, EventIds.RateLimits,
                $"Retrieving rate-limit bucket for [{uri.LocalPath}]");

            if (Ratelimits.TryGetValue(uri.LocalPath, out var bucket) && !bucket.HasExpired && bucket.Remaining <= 0)
            {
                Configuration.Logger.Log(LogLevel.Warning, EventIds.RateLimits,
                    $"Pre-emptive rate-limit bucket hit for [{uri.LocalPath}]: [{bucket.Limit - bucket.Remaining}/{bucket.Limit}]");

                if (!Configuration.ThrowOnRateLimits)
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
                Ratelimits.TryAdd(uri.LocalPath, bucket);
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

            Configuration.Logger.Log(LogLevel.Debug, EventIds.RateLimits,
                $"Rate-limit bucket passed for [{uri.LocalPath}]: [{bucket.Limit - bucket.Remaining}/{bucket.Limit}]");
        }

        internal async Task<T> GetAsync<T>(Uri route, IReadOnlyDictionary<string, string> parameters = null)
            where T : class
        {
            parameters ??= new Dictionary<string, string>();

            var paramsString = string.Join(" | ", parameters.Select(x => $"{x.Key}:{x.Value}"));
            Configuration.Logger.Log(LogLevel.Information, EventIds.RestApi,
                $"Getting [{route.LocalPath}] with parameters [{(string.IsNullOrWhiteSpace(paramsString) ? "no_params" : paramsString)}]");

            if (parameters is { Count: > 0 })
            {
                route = new Uri(route, $"?{string.Join("&", parameters.Select(x => $"{x.Key}={x.Value}"))}");
            }

            var bucket = await GetBucketFromUriAsync(route).ConfigureAwait(false);
            var response = await HttpClient.GetAsync(route).ConfigureAwait(false);

            return await HandleResponseAsync<T>(route, response, bucket).ConfigureAwait(false);
        }

        internal async Task<T> PostAsync<T>(Uri route, IReadOnlyDictionary<string, string> parameters) where T : class
        {
            var paramsString = string.Join(" | ", parameters.Select(x => $"{x.Key}:{x.Value}"));
            Configuration.Logger.Log(LogLevel.Information, EventIds.RestApi,
                $"Posting [{route.LocalPath}] with parameters [{paramsString}]");

            var bucket = await GetBucketFromUriAsync(route).ConfigureAwait(false);
            var response = await HttpClient.PostAsync(route, new FormUrlEncodedContent(parameters))
                .ConfigureAwait(false);

            return await HandleResponseAsync<T>(route, response, bucket).ConfigureAwait(false);
        }

        internal async Task<T> HandleResponseAsync<T>(Uri route, HttpResponseMessage response, RatelimitBucket bucket) where T : class
        {
            if (!response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                throw new ApiException(response.ReasonPhrase, response.StatusCode, jsonResponse);
            }

            var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
            var streamReader = new StreamReader(stream);
            var content = await streamReader.ReadToEndAsync().ConfigureAwait(false);

            Configuration.Logger.Log(LogLevel.Trace, EventIds.RestApi, $"Response received: {content}");

            UpdateBucket(route, bucket, response);
            return Serializer.Deserialize<T>(stream);
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            ThrowIfDisposed();
            _disposed = true;
            HttpClient?.Dispose();
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(OsuClient), "The client is disposed.");
        }
    }
}