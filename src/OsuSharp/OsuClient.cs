using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using OsuSharp.Entities;
using OsuSharp.Enums;
using OsuSharp.Logging;
using OsuSharp.Net;

namespace OsuSharp
{
    public sealed class OsuClient : IDisposable
    {
        internal readonly OsuClientConfiguration Configuration;
        internal readonly RequestHandler Handler;

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
                Handler.UpdateAuthorizationHeader(_credentials);
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
            Handler = new RequestHandler(this);
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
            var response = await Handler.PostAsync<AccessTokenResponse>(uri, parameters).ConfigureAwait(false);

            return Credentials = new OsuToken
            {
                Type = Enum.Parse<TokenType>(response.TokenType),
                AccessToken = response.AccessToken,
                ExpiresInSeconds = response.ExpiresIn
            };
        }

        /// <summary>
        ///     Gets a user from the API.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Defaults gamemode is picked when null.</param>
        /// <returns>
        ///     Returns a <see cref="User"/>.
        /// </returns>
        public async Task<User> GetUserAsync(
            [NotNull] string username, 
            [MaybeNull] GameMode? gameMode = null)
        {
            ThrowIfDisposed();
            await GetOrUpdateAccessTokenAsync();

            Uri.TryCreate(
                $"{Endpoints.Domain}{Endpoints.Api}{Endpoints.Users}/{username}/{gameMode.ToString() ?? ""}", 
                UriKind.Absolute, out var uri);
            
            return await Handler.GetAsync<User>(uri);
        }
        
        /// <summary>
        ///     Gets a user from the API.
        /// </summary>
        /// <param name="id">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Defaults gamemode is picked when null.</param>
        /// <returns>
        ///     Returns a <see cref="User"/>.
        /// </returns>
        public async Task<User> GetUserAsync(
            [NotNull] long id, 
            [MaybeNull] GameMode? gameMode = null)
        {
            ThrowIfDisposed();
            await GetOrUpdateAccessTokenAsync();
            
            Uri.TryCreate(
                $"{Endpoints.Domain}{Endpoints.Api}{Endpoints.Users}/{id}/{gameMode.ToString() ?? ""}", 
                UriKind.Absolute, out var uri);
            
            return await Handler.GetAsync<User>(uri);
        }

        /// <inheritdoc cref="IDisposable.Dispose"/>
        public void Dispose()
        {
            ThrowIfDisposed();
            _disposed = true;
            Handler.Dispose();
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(OsuClient), "The client is disposed.");
        }
    }
}