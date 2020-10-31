using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OsuSharp.Entities;
using OsuSharp.Enums;
using OsuSharp.Exceptions;

namespace OsuSharp
{
    public sealed class OsuClient
    {
        private readonly ConcurrentDictionary<string, RatelimitBucket> _ratelimits;
        private readonly OsuClientConfiguration _configuration;
        private readonly HttpClient _httpClient;

        /// <summary>
        ///     Gets the current used credentials to communicate with the API.
        /// </summary>
        public OsuToken Credentials { get; internal set; }

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
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _httpClient = new HttpClient();
            _ratelimits = new ConcurrentDictionary<string, RatelimitBucket>();
        }

        /// <summary>
        ///     Gets or requests an API access token.
        /// </summary>
        public async ValueTask<OsuToken> GetOrUpdateAccessTokenAsync()
        {
            if (!Credentials.HasExpired)
            {
                return Credentials;
            }

            var parameters = new Dictionary<string, string>
            {
                ["client_id"] = _configuration.ClientId.ToString(),
                ["client_secret"] = _configuration.ClientSecret,
                ["grant_type"] = "client_credentials",
                ["scope"] = "public"
            };

            Uri.TryCreate($"{Endpoints.Domain}{Endpoints.Oauth}{Endpoints.Token}", UriKind.Absolute, out var uri);
            var response = await PostAsync<AccessTokenResponse>(parameters, uri).ConfigureAwait(false);

            return Credentials = new OsuToken
            {
                Type = Enum.Parse<TokenType>(response.AccessToken),
                AccessToken = response.AccessToken,
                ExpiresInSeconds = response.ExpiresIn
            };
        }

        internal async Task<RatelimitBucket> GetBucketFromUriAsync(Uri uri)
        {
            if (_ratelimits.TryGetValue(uri.LocalPath, out var bucket) && !bucket.HasExpired && bucket.Remaining <= 0)
            {
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
                _ratelimits.TryAdd(uri.LocalPath, bucket);
            }

            return bucket;
        }

        internal void UpdateBucket(RatelimitBucket bucket, HttpResponseMessage response)
        {
            if (bucket.HasExpired)
            {
                bucket.CreatedAt = DateTimeOffset.Now;

                if (response.Headers.TryGetValues("X-RateLimit-Limit", out var limitHeaders))
                {
                    bucket.Limit = int.Parse(limitHeaders.First());
                }

                if (response.Headers.TryGetValues("X-RateLimit-Remaining", out var remainingHeaders))
                {
                    bucket.Remaining = int.Parse(remainingHeaders.First());
                }
            }
            else
            {
                bucket.Remaining--;
            }
        }

        internal async Task<T> PostAsync<T>(IReadOnlyDictionary<string, string> parameters, Uri uri)
        {
            var bucket = await GetBucketFromUriAsync(uri).ConfigureAwait(false);
            var response = await _httpClient.PostAsync(uri, new FormUrlEncodedContent(parameters))
                .ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException(response.ReasonPhrase, response.StatusCode);
            }

            UpdateBucket(bucket, response);

            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
}