using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OsuSharp.Entities;
using OsuSharp.Enums;

namespace OsuSharp
{
    public sealed class OsuClient
    {
        private readonly OsuClientConfiguration _configuration;
        private readonly HttpClient _httpClient;
        
        /// <summary>
        ///     Gets the current used credentials to communicate with the API.
        /// </summary>
        public OsuToken Credentials { get; set; }

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
        }

        /// <summary>
        ///     Gets or requests an API access token.
        /// </summary>
        /// <remarks>
        ///     If the <see cref="OsuToken"/> was provided by the user, this method will refresh it.
        /// </remarks>
        public async ValueTask<OsuToken> GetOrUpdateAccessTokenAsync()
        {
            if (!Credentials.HasExpired && Credentials.IsInternal)
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

            var response = await _httpClient.PostAsync(
                $"{Endpoints.DOMAIN}{Endpoints.OAUTH}{Endpoints.TOKEN}",
                new FormUrlEncodedContent(parameters));

            if (!response.IsSuccessStatusCode)
            {
                throw new ApiException(response.ReasonPhrase, response.StatusCode);
            }

            var content = await response.Content.ReadAsStringAsync();
            var accessTokenResponse = JsonConvert.DeserializeObject<AccessTokenResponse>(content);

            return Credentials = new OsuToken
            {
                Type = Enum.Parse<TokenType>(accessTokenResponse.AccessToken),
                AccessToken = accessTokenResponse.AccessToken,
                ExpiresInSeconds = accessTokenResponse.ExpiresIn,
                IsInternal = true
            };
        }
    }
}