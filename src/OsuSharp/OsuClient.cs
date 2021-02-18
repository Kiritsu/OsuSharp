using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading.Tasks;
using OsuSharp.Entities;
using OsuSharp.Entities.Event;
using OsuSharp.Enums;
using OsuSharp.Extensions;
using OsuSharp.Logging;
using OsuSharp.Net;

namespace OsuSharp
{
    public sealed class OsuClient : IDisposable
    {
        internal readonly OsuClientConfiguration Configuration;
        private readonly RequestHandler _handler;
        private OsuToken _credentials;
        private bool _disposed;

        /// <summary>
        ///     Gets the current used credentials to communicate with the API.
        /// </summary>
        public OsuToken Credentials
        {
            get => _credentials;
            internal set
            {
                _credentials = value ?? throw new ArgumentNullException(nameof(Credentials), "Credentials cannot become null.");
                _handler.UpdateAuthorizationHeader(_credentials);
            }
        }
        
        /// <summary>
        ///     Initializes a new OsuClient with the given configuration.
        /// </summary>
        /// <param name="configuration">
        ///     Configuration of the client.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <see cref="configuration" /> is null
        /// </exception>
        public OsuClient(
            [NotNull] OsuClientConfiguration configuration)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            Configuration.Logger ??= new DefaultLogger(Configuration);
            _handler = new RequestHandler(this);
        }

        /// <inheritdoc cref="IDisposable.Dispose" />
        public void Dispose()
        {
            ThrowIfDisposed();
            _disposed = true;
            _handler.Dispose();
        }

        /// <summary>
        ///     Gets or requests an API access token. This method will use Client Credential Grant unless
        ///     A refresh token is present on the current <see cref="OsuToken" /> instance.
        /// </summary>
        /// <returns>
        ///     Returns an <see cref="OsuToken" />.
        /// </returns>
        public async ValueTask<OsuToken> GetOrUpdateAccessTokenAsync()
        {
            ThrowIfDisposed();

            if (Credentials != null && !Credentials.HasExpired)
            {
                return Credentials;
            }

            var parameters = new Dictionary<string, string>
            {
                ["client_id"] = Configuration.ClientId.ToString(),
                ["client_secret"] = Configuration.ClientSecret
            };

            if (Credentials == null || string.IsNullOrWhiteSpace(Credentials.RefreshToken))
            {
                parameters["grant_type"] = "client_credentials";
                parameters["scope"] = "public";
            }
            else
            {
                parameters["grant_type"] = "refresh_token";
                parameters["refresh_token"] = Credentials.RefreshToken;
            }

            Uri.TryCreate($"{Endpoints.TokenEndpoint}", UriKind.Relative, out var uri);
            var response = await _handler.SendAsync<AccessTokenResponse>(new OsuApiRequest
            {
                Endpoint = Endpoints.TokenEndpoint,
                Method = HttpMethod.Post,
                Route = uri,
                Parameters = parameters
            }).ConfigureAwait(false);

            return Credentials = new OsuToken
            {
                Type = Enum.Parse<TokenType>(response.TokenType),
                AccessToken = response.AccessToken,
                ExpiresInSeconds = response.ExpiresIn
            };
        }

        /// <summary>
        ///     Updates the current osu! api credentials by the given access, refresh tokens and the expiry time.
        /// </summary>
        /// <param name="accessToken">
        ///     Access token.
        /// </param>
        /// <param name="refreshToken">
        ///     Refresh token.
        /// </param>
        /// <param name="expiresIn">
        ///     Amount of seconds before the token expires.
        /// </param>
        /// <returns>
        ///     Returns an <see cref="OsuToken" />.
        /// </returns>
        /// <remarks>
        ///     If you are going to use the authorization code grant, use this method to create your <see cref="OsuToken" />.
        /// </remarks>
        public OsuToken UpdateAccessToken(
            [NotNull] string accessToken,
            [NotNull] string refreshToken,
            long expiresIn)
        {
            ThrowIfDisposed();

            return Credentials = new OsuToken
            {
                Type = TokenType.Bearer,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresInSeconds = expiresIn
            };
        }

        /// <summary>
        ///     Revokes the current access token.
        /// </summary>
        public async Task RevokeAccessTokenAsync()
        {
            ThrowIfDisposed();

            Uri.TryCreate(
                $"{Endpoints.CurrentTokensEndpoint}",
                UriKind.Relative, out var uri);

            await _handler.SendAsync(new OsuApiRequest
            {
                Endpoint = Endpoints.CurrentTokensEndpoint,
                Method = HttpMethod.Delete,
                Route = uri
            }).ConfigureAwait(false);

            if (Credentials is not null)
            {
                Credentials.Revoked = true;
            }
        }

        /// <summary>
        ///     Gets a user's kudosu history from the API.
        /// </summary>
        /// <param name="username">
        ///     Username of the user.
        /// </param>
        /// <param name="limit">
        ///     Limit number of results.
        /// </param>
        /// <param name="offset">
        ///     Offset of result for pagination.
        /// </param>
        /// <returns>
        ///     Returns a set of KudosuHistory
        /// </returns>
        public async Task<IReadOnlyList<KudosuHistory>> GetUserKudosuAsync(
            [NotNull] string username,
            Optional<int> limit = default,
            Optional<int> offset = default)
        {
            ThrowIfDisposed();
            await GetOrUpdateAccessTokenAsync();

            Uri.TryCreate(
                string.Format(Endpoints.UserKudosuEndpoint, username),
                UriKind.Relative, out var uri);

            Dictionary<string, string> parameters = new();
            if (limit.HasValue)
            {
                parameters["limit"] = limit.Value.ToString();
            }

            if (offset.HasValue)
            {
                parameters["offset"] = offset.Value.ToString();
            }

            return await _handler.SendAsync<IReadOnlyList<KudosuHistory>>(new OsuApiRequest
            {
                Endpoint = Endpoints.UserEndpoint,
                Method = HttpMethod.Get,
                Route = uri,
                Parameters = parameters
            }).ConfigureAwait(false);
        }

        /// <summary>
        ///     Gets a user's kudosu history from the API.
        /// </summary>
        /// <param name="userId">
        ///     Id of the user.
        /// </param>
        /// <param name="limit">
        ///     Limit number of results.
        /// </param>
        /// <param name="offset">
        ///     Offset of result for pagination.
        /// </param>
        /// <returns>
        ///     Returns a set of KudosuHistory
        /// </returns>
        public async Task<IReadOnlyList<KudosuHistory>> GetUserKudosuAsync(
            long userId,
            Optional<int> limit = default,
            Optional<int> offset = default)
        {
            ThrowIfDisposed();
            await GetOrUpdateAccessTokenAsync();

            Uri.TryCreate(
                string.Format(Endpoints.UserKudosuEndpoint, userId),
                UriKind.Relative, out var uri);

            Dictionary<string, string> parameters = new();
            if (limit.HasValue)
            {
                parameters["limit"] = limit.Value.ToString();
            }

            if (offset.HasValue)
            {
                parameters["offset"] = offset.Value.ToString();
            }

            return await _handler.SendAsync<IReadOnlyList<KudosuHistory>>(new OsuApiRequest
            {
                Endpoint = Endpoints.UserEndpoint,
                Method = HttpMethod.Get,
                Route = uri,
                Parameters = parameters
            }).ConfigureAwait(false);
        }

        /// <summary>
        ///     Gets a user from the API.
        /// </summary>
        /// <param name="username">
        ///     Username of the user.
        /// </param>
        /// <param name="gameMode">
        ///     Gamemode of the user. Defaults gamemode is picked when null.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="User" />.
        /// </returns>
        public async Task<User> GetUserAsync(
            [NotNull] string username,
            Optional<GameMode> gameMode = default)
        {
            ThrowIfDisposed();
            await GetOrUpdateAccessTokenAsync();

            Uri.TryCreate(
                $"{Endpoints.UserEndpoint}/{username}/{gameMode.ToApiString()}",
                UriKind.Relative, out var uri);

            return await _handler.SendAsync<User>(new OsuApiRequest
            {
                Endpoint = Endpoints.UserEndpoint,
                Method = HttpMethod.Get,
                Route = uri
            }).ConfigureAwait(false);
        }

        /// <summary>
        ///     Gets a user from the API.
        /// </summary>
        /// <param name="id">
        ///     Id of the user.
        /// </param>
        /// <param name="gameMode">
        ///     Gamemode of the user. Defaults gamemode is picked when null.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="User" />.
        /// </returns>
        public async Task<User> GetUserAsync(
            long id,
            Optional<GameMode> gameMode = default)
        {
            ThrowIfDisposed();
            await GetOrUpdateAccessTokenAsync();

            Uri.TryCreate(
                $"{Endpoints.UserEndpoint}/{id}/{gameMode.ToApiString()}",
                UriKind.Relative, out var uri);

            return await _handler.SendAsync<User>(new OsuApiRequest
            {
                Endpoint = Endpoints.UserEndpoint,
                Method = HttpMethod.Get,
                Route = uri
            }).ConfigureAwait(false);
        }

        /// <summary>
        ///     Gets a user's recent activity history from the API.
        /// </summary>
        /// <param name="id">
        ///     Id of the user.
        /// </param>
        /// <param name="limit">
        ///     Limit number of results.
        /// </param>
        /// <param name="offset">
        ///     Offset of result for pagination.
        /// </param>
        /// <returns>
        ///     Returns a set of Events
        /// </returns>
        public async Task<IReadOnlyList<Event>> GetUserRecentAsync(
            long id,
            Optional<int> limit = default,
            Optional<int> offset = default)
        {
            ThrowIfDisposed();
            await GetOrUpdateAccessTokenAsync();
            
            Uri.TryCreate(
                string.Format(Endpoints.UserRecentEndpoint, id),
                UriKind.Relative, out var uri);
            
            Dictionary<string, string> parameters = new();
            if (limit.HasValue)
            {
                parameters["limit"] = limit.Value.ToString();
            }

            if (offset.HasValue)
            {
                parameters["offset"] = offset.Value.ToString();
            }

            return await _handler.SendAsync<IReadOnlyList<Event>>(new OsuApiRequest
            {
                Endpoint = Endpoints.UserEndpoint,
                Method = HttpMethod.Get,
                Route = uri,
                Parameters = parameters
            }).ConfigureAwait(false);
        }

        /// <summary>
        ///     Gets the current authenticated user from the API.
        /// </summary>
        /// <param name="gameMode">
        ///     Gamemode of the user. Defaults gamemode is picked when null.
        /// </param>
        /// <returns>
        ///     Returns a <see cref="User" />.
        /// </returns>
        public async Task<User> GetCurrentUserAsync(
            Optional<GameMode> gameMode = default)
        {
            ThrowIfDisposed();
            await GetOrUpdateAccessTokenAsync();

            Uri.TryCreate(
                $"{Endpoints.CurrentEndpoint}/{gameMode.ToApiString()}",
                UriKind.Relative, out var uri);

            return await _handler.SendAsync<User>(new OsuApiRequest
            {
                Endpoint = Endpoints.CurrentEndpoint,
                Method = HttpMethod.Get,
                Route = uri
            }).ConfigureAwait(false);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(OsuClient), "The client is disposed.");
        }
    }
}