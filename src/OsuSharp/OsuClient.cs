using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using OsuSharp.Domain;
using OsuSharp.Extensions;
using OsuSharp.Interfaces;
using OsuSharp.JsonModels;
using OsuSharp.Models;
using OsuSharp.Net;

namespace OsuSharp
{
    /// <summary>
    /// Represents an implementation of an OsuClient for communicating with the osu! api v2.
    /// </summary>
    public sealed class OsuClient : IOsuClient
    {
        private readonly IRequestHandler _handler;
        private bool _disposed;
        private OsuToken _credentials;

        /// <summary>
        /// Gets the configuration of the client.
        /// </summary>
        public IOsuClientConfiguration Configuration { get; }

        /// <summary>
        /// Initializes a new OsuClient with the given configuration.
        /// </summary>
        /// <param name="configuration">
        /// Configuration of the client.
        /// </param>
        /// <param name="handler">
        /// Request handler of the client.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <see cref="configuration" /> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <see cref="handler"/> is null.
        /// </exception>
        public OsuClient(
            [NotNull] IOsuClientConfiguration configuration,
            [NotNull] IRequestHandler handler)
        {
            Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _handler = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        /// <inheritdoc cref="IDisposable.Dispose" />
        public void Dispose()
        {
            ThrowIfDisposed();
            _disposed = true;
            _handler.Dispose();
        }

        /// <summary>
        /// Gets or requests an API access token. This method will use Client Credential Grant unless
        /// A refresh token is present on the current <see cref="IOsuToken" /> instance.
        /// </summary>
        /// <param name="token">
        /// Cancellation token.
        /// </param>
        /// <returns>
        /// Returns an <see cref="IOsuToken" />.
        /// </returns>
        public async ValueTask<IOsuToken> GetOrUpdateAccessTokenAsync(
            CancellationToken token = default)
        {
            ThrowIfDisposed();

            if (_credentials is { HasExpired: false })
            {
                return _credentials;
            }

            var parameters = new Dictionary<string, string>
            {
                ["client_id"] = Configuration.ClientId.ToString(),
                ["client_secret"] = Configuration.ClientSecret
            };

            if (_credentials == null || string.IsNullOrWhiteSpace(_credentials.RefreshToken))
            {
                parameters["grant_type"] = "client_credentials";
                parameters["scope"] = "public";
            }
            else
            {
                parameters["grant_type"] = "refresh_token";
                parameters["refresh_token"] = _credentials.RefreshToken;
            }

            Uri.TryCreate($"{Endpoints.TokenEndpoint}", UriKind.Relative, out var uri);
            var response = await _handler.SendAsync<AccessTokenResponse, AccessTokenResponseJsonModel>(new OsuApiRequest
            {
                Endpoint = Endpoints.TokenEndpoint,
                Method = HttpMethod.Post,
                Route = uri,
                Parameters = parameters,
                Token = _credentials
            }, token).ConfigureAwait(false);

            return _credentials = new OsuToken
            {
                Type = Enum.Parse<TokenType>(response.TokenType),
                AccessToken = response.AccessToken,
                ExpiresInSeconds = response.ExpiresIn,
                RefreshToken = response.RefreshToken
            };
        }

        /// <summary>
        /// Updates the current osu! api credentials by the given access, refresh tokens and the expiry time.
        /// </summary>
        /// <param name="accessToken">
        /// Access token.
        /// </param>
        /// <param name="refreshToken">
        /// Refresh token.
        /// </param>
        /// <param name="expiresIn">
        /// Amount of seconds before the token expires.
        /// </param>
        /// <returns>
        /// Returns an <see cref="IOsuToken" />.
        /// </returns>
        /// <remarks>
        /// If you are going to use the authorization code grant, use this method to create your <see cref="IOsuToken" />.
        /// </remarks>
        public IOsuToken UpdateAccessToken(
            [NotNull] string accessToken,
            [NotNull] string refreshToken,
            long expiresIn)
        {
            ThrowIfDisposed();

            return _credentials = new OsuToken
            {
                Type = TokenType.Bearer,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresInSeconds = expiresIn
            };
        }

        /// <summary>
        /// Revokes the current access token.
        /// </summary>
        public async Task RevokeAccessTokenAsync(
            CancellationToken token = default)
        {
            ThrowIfDisposed();

            Uri.TryCreate(
                $"{Endpoints.CurrentTokensEndpoint}",
                UriKind.Relative, out var uri);

            await _handler.SendAsync(new OsuApiRequest
            {
                Endpoint = Endpoints.CurrentTokensEndpoint,
                Method = HttpMethod.Delete,
                Route = uri,
                Token = _credentials
            }, token).ConfigureAwait(false);

            if (_credentials is not null)
            {
                _credentials.Revoked = true;
            }
        }

        /// <summary>
        /// Gets a user's kudosu history from the API.
        /// </summary>
        /// <param name="userId">
        /// Id of the user.
        /// </param>
        /// <param name="limit">
        /// Limit number of results.
        /// </param>
        /// <param name="offset">
        /// Offset of result for pagination.
        /// </param>
        /// <returns>
        /// Returns a set of <see cref="IKudosuHistory" />.
        /// </returns>
        public async Task<IReadOnlyList<IKudosuHistory>> GetUserKudosuAsync(
            long userId,
            int? limit = null,
            int? offset = null,
            CancellationToken token = default)
        {
            ThrowIfDisposed();
            await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

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

            return await _handler.SendAsync<List<KudosuHistory>, List<KudosuHistory>>(new OsuApiRequest
            {
                Endpoint = Endpoints.UserEndpoint,
                Method = HttpMethod.Get,
                Route = uri,
                Parameters = parameters,
                Token = _credentials
            }, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a user from the API.
        /// </summary>
        /// <param name="username">
        /// Username of the user.
        /// </param>
        /// <param name="gameMode">
        /// Gamemode of the user. Defaults gamemode is picked when null.
        /// </param>
        /// <returns>
        /// Returns a <see cref="IUser" />.
        /// </returns>
        public async Task<IUser> GetUserAsync(
            [NotNull] string username,
            GameMode? gameMode = null,
            CancellationToken token = default)
        {
            ThrowIfDisposed();
            await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

            Uri.TryCreate(
                $"{Endpoints.UserEndpoint}/{username}/{gameMode.ToApiString()}?key=username",
                UriKind.Relative, out var uri);

            return await _handler.SendAsync<User, UserJsonModel>(new OsuApiRequest
            {
                Endpoint = Endpoints.UserEndpoint,
                Method = HttpMethod.Get,
                Route = uri,
                Token = _credentials
            }, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a user from the API.
        /// </summary>
        /// <param name="userId">
        /// Id of the user.
        /// </param>
        /// <param name="gameMode">
        /// Gamemode of the user. Defaults gamemode is picked when null.
        /// </param>
        /// <returns>
        /// Returns a <see cref="IUser" />.
        /// </returns>
        public async Task<IUser> GetUserAsync(
            long userId,
            GameMode? gameMode = null,
            CancellationToken token = default)
        {
            ThrowIfDisposed();
            await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

            Uri.TryCreate(
                $"{Endpoints.UserEndpoint}/{userId}/{gameMode.ToApiString()}?key=id",
                UriKind.Relative, out var uri);

            return await _handler.SendAsync<User, UserJsonModel>(new OsuApiRequest
            {
                Endpoint = Endpoints.UserEndpoint,
                Method = HttpMethod.Get,
                Route = uri,
                Token = _credentials
            }, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a user's recent activity history from the API.
        /// </summary>
        /// <param name="userId">
        /// Id of the user.
        /// </param>
        /// <param name="limit">
        /// Limit number of results.
        /// </param>
        /// <param name="offset">
        /// Offset of result for pagination.
        /// </param>
        /// <returns>
        /// Returns a set of <see cref="IEvent" />s.
        /// </returns>
        public async Task<IReadOnlyList<IEvent>> GetUserRecentAsync(
            long userId,
            int? limit = null,
            int? offset = null,
            CancellationToken token = default)
        {
            ThrowIfDisposed();
            await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

            Uri.TryCreate(
                string.Format(Endpoints.UserRecentEndpoint, userId),
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

            return await _handler.SendAsync<List<Event>, List<EventJsonModel>>(new OsuApiRequest
            {
                Endpoint = Endpoints.UserEndpoint,
                Method = HttpMethod.Get,
                Route = uri,
                Parameters = parameters,
                Token = _credentials
            }, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a user's beatmapsets from the API.
        /// </summary>
        /// <param name="userId">
        /// Id of the user.
        /// </param>
        /// <param name="type">
        /// Type of the beatmapsets to look-up.
        /// </param>
        /// <param name="limit">
        /// Limit number of results.
        /// </param>
        /// <param name="offset">
        /// Offset of result for pagination.
        /// </param>
        /// <returns>
        /// Returns a set of <see cref="IBeatmapset" />s.
        /// </returns>
        public async Task<IReadOnlyList<IBeatmapset>> GetUserBeatmapsetsAsync(
            long userId,
            BeatmapsetType type,
            int? limit = null,
            int? offset = null,
            CancellationToken token = default)
        {
            ThrowIfDisposed();
            await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

            Uri.TryCreate(
                string.Format(Endpoints.UserBeatmapsetsEndpoint, userId, type.ToApiString()),
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

            return await _handler.SendAsync<List<Beatmapset>, List<BeatmapsetJsonModel>>(new OsuApiRequest
            {
                Endpoint = Endpoints.UserEndpoint,
                Method = HttpMethod.Get,
                Route = uri,
                Parameters = parameters,
                Token = _credentials
            }, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a user's scores from the API.
        /// </summary>
        /// <param name="userId">
        /// Id of the user.
        /// </param>
        /// <param name="type">
        /// Type of the scores to look-up.
        /// </param>
        /// <param name="includeFails">
        /// Whether to include failed scores.
        /// </param>
        /// <param name="gameMode">
        /// Game mode to lookup scores for.
        /// </param>
        /// <param name="limit">
        /// Limit number of results.
        /// </param>
        /// <param name="offset">
        /// Offset of result for pagination.
        /// </param>
        /// <returns>
        /// Returns a set of <see cref="IScore" />s.
        /// </returns>
        public async Task<IReadOnlyList<IScore>> GetUserScoresAsync(
            long userId,
            ScoreType type,
            bool includeFails = false,
            GameMode? gameMode = null,
            int? limit = null,
            int? offset = null,
            CancellationToken token = default)
        {
            ThrowIfDisposed();
            await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

            Uri.TryCreate(
                string.Format(Endpoints.UserScoresEndpoint, userId, type.ToApiString()),
                UriKind.Relative, out var uri);

            Dictionary<string, string> parameters = new();
            if (includeFails)
            {
                parameters["include_fails"] = "1";
            }

            if (gameMode.HasValue)
            {
                parameters["mode"] = gameMode.Value.ToApiString();
            }

            if (limit.HasValue)
            {
                parameters["limit"] = limit.Value.ToString();
            }

            if (offset.HasValue)
            {
                parameters["offset"] = offset.Value.ToString();
            }

            return await _handler.SendAsync<List<Score>, List<ScoreJsonModel>>(new OsuApiRequest
            {
                Endpoint = Endpoints.UserEndpoint,
                Method = HttpMethod.Get,
                Route = uri,
                Parameters = parameters,
                Token = _credentials
            }, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets the current authenticated user from the API.
        /// </summary>
        /// <param name="gameMode">
        /// Gamemode of the user. Defaults gamemode is picked when null.
        /// </param>
        /// <returns>
        /// Returns a <see cref="IUser" />.
        /// </returns>
        public async Task<IUser> GetCurrentUserAsync(
            GameMode? gameMode = null,
            CancellationToken token = default)
        {
            ThrowIfDisposed();
            await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

            Uri.TryCreate(
                $"{Endpoints.CurrentEndpoint}/{gameMode.ToApiString()}",
                UriKind.Relative, out var uri);

            return await _handler.SendAsync<User, UserJsonModel>(new OsuApiRequest
            {
                Endpoint = Endpoints.CurrentEndpoint,
                Method = HttpMethod.Get,
                Route = uri,
                Token = _credentials
            }, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a beatmap by its id from the API.
        /// </summary>
        /// <param name="beatmapId">
        /// Id of the beatmap.
        /// </param>
        /// <returns>
        /// Returns a <see cref="IBeatmap" />.
        /// </returns>
        public async Task<IBeatmap> GetBeatmapAsync(
            long beatmapId,
            CancellationToken token = default)
        {
            ThrowIfDisposed();
            await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

            Uri.TryCreate(
                string.Format(Endpoints.BeatmapsEndpoint, beatmapId),
                UriKind.Relative, out var uri);

            return await _handler.SendAsync<Beatmap, BeatmapJsonModel>(new OsuApiRequest
            {
                Endpoint = Endpoints.Beatmaps,
                Method = HttpMethod.Get,
                Route = uri,
                Token = _credentials
            }, token).ConfigureAwait(false);
        }

        /// <summary>
        /// Gets a beatmapset by its id from the API.
        /// </summary>
        /// <param name="beatmapsetId">
        /// Id of the beatmap.
        /// </param>
        /// <returns>
        /// Returns a <see cref="IBeatmapset" />.
        /// </returns>
        public async Task<IBeatmapset> GetBeatmapsetAsync(
            long beatmapsetId,
            CancellationToken token = default)
        {
            ThrowIfDisposed();
            await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

            Uri.TryCreate(
                string.Format(Endpoints.BeatmapsetsEndpoint, beatmapsetId),
                UriKind.Relative, out var uri);

            return await _handler.SendAsync<Beatmapset, BeatmapsetJsonModel>(new OsuApiRequest
            {
                Endpoint = Endpoints.Beatmaps,
                Method = HttpMethod.Get,
                Route = uri,
                Token = _credentials
            }, token).ConfigureAwait(false);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(OsuClient), "The client is disposed.");
            }
        }
    }
}