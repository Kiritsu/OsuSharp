using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using OsuSharp.Domain;
using OsuSharp.Extensions;
using OsuSharp.Interfaces;
using OsuSharp.JsonModels;
using OsuSharp.Models;
using OsuSharp.Net;

namespace OsuSharp;

/// <summary>
/// Represents an implementation of an OsuClient for communicating with the osu! api v2.
/// </summary>
public sealed class OsuClient : IOsuClient
{
    private readonly IRequestHandler _handler;
    private bool _disposed;
    private OsuToken? _credentials;

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
        IOsuClientConfiguration configuration,
        IRequestHandler handler)
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
            Route = uri!,
            Parameters = parameters,
            Token = _credentials,
            Client = this
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
    /// Gets an access token from authorization code grant.
    /// </summary>
    /// <param name="code">
    /// The code given by the authorization code grant.
    /// </param>
    /// <param name="redirectUri">
    /// The redirect uri where users are to be sent after authorization.
    /// </param>
    /// <param name="token">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Returns an <see cref="IOsuToken" />.
    /// </returns>
    public async Task<IOsuToken> GetAccessTokenFromCodeAsync(
        string code,
        string redirectUri,
        CancellationToken token = default)
    {
        ThrowIfDisposed();

        var parameters = new Dictionary<string, string>
        {
            ["client_id"] = Configuration.ClientId.ToString(),
            ["client_secret"] = Configuration.ClientSecret,
            ["code"] = code,
            ["grant_type"] = "authorization_code",
            ["redirect_uri"] = redirectUri
        };

        Uri.TryCreate($"{Endpoints.TokenEndpoint}", UriKind.Relative, out var uri);
        var response = await _handler.SendAsync<AccessTokenResponse, AccessTokenResponseJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.TokenEndpoint,
            Method = HttpMethod.Post,
            Route = uri!,
            Parameters = parameters,
            Token = _credentials,
            Client = this
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
        string accessToken,
        string refreshToken,
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
            Route = uri!,
            Token = _credentials,
            Client = this
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
    /// <param name="token">
    /// Cancellation token.
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

        return await _handler.SendMultipleAsync<KudosuHistory, KudosuHistory>(new OsuApiRequest
        {
            Endpoint = Endpoints.UserEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Parameters = parameters,
            Token = _credentials,
            Client = this
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
    /// <param name="token">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Returns a <see cref="IGlobalUser" />.
    /// </returns>
    public async Task<IGlobalUser> GetUserAsync(
        string username,
        GameMode? gameMode = null,
        CancellationToken token = default)
    {
        ThrowIfDisposed();
        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        Uri.TryCreate(
            $"{Endpoints.UserEndpoint}/{username}/{gameMode.ToApiString()}?key=username",
            UriKind.Relative, out var uri);

        return await _handler.SendAsync<GlobalUser, UserJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.UserEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Client = this
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
    /// <param name="token">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Returns a <see cref="IGlobalUser" />.
    /// </returns>
    public async Task<IGlobalUser> GetUserAsync(
        long userId,
        GameMode? gameMode = null,
        CancellationToken token = default)
    {
        ThrowIfDisposed();
        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        Uri.TryCreate(
            $"{Endpoints.UserEndpoint}/{userId}/{gameMode.ToApiString()}?key=id",
            UriKind.Relative, out var uri);

        return await _handler.SendAsync<GlobalUser, UserJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.UserEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Client = this
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
    /// <param name="token">
    /// Cancellation token.
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

        return await _handler.SendMultipleAsync<Event, EventJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.UserEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Parameters = parameters,
            Token = _credentials,
            Client = this
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
    /// <param name="token">
    /// Cancellation token.
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

        return await _handler.SendMultipleAsync<Beatmapset, BeatmapsetJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.UserEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Parameters = parameters,
            Token = _credentials,
            Client = this
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
    /// <param name="token">
    /// Cancellation token.
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

        return await _handler.SendMultipleAsync<Score, ScoreJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.UserEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Parameters = parameters,
            Token = _credentials,
            Client = this
        }, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets the current authenticated user from the API.
    /// </summary>
    /// <param name="gameMode">
    /// Gamemode of the user. Defaults gamemode is picked when null.
    /// </param>
    /// <param name="token">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Returns a <see cref="IGlobalUser" />.
    /// </returns>
    public async Task<IGlobalUser> GetCurrentUserAsync(
        GameMode? gameMode = null,
        CancellationToken token = default)
    {
        ThrowIfDisposed();
        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        Uri.TryCreate(
            $"{Endpoints.CurrentEndpoint}/{gameMode.ToApiString()}",
            UriKind.Relative, out var uri);

        return await _handler.SendAsync<GlobalUser, UserJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.CurrentEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Client = this
        }, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a beatmap by its id from the API.
    /// </summary>
    /// <param name="beatmapId">
    /// Id of the beatmap.
    /// </param>
    /// <param name="token">
    /// Cancellation token.
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
            string.Format(Endpoints.BeatmapsBeatmapEndpoint, beatmapId),
            UriKind.Relative, out var uri);

        return await _handler.SendAsync<Beatmap, BeatmapJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.BeatmapsBeatmapEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Client = this
        }, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a beatmapset by its id from the API.
    /// </summary>
    /// <param name="beatmapsetId">
    /// Id of the beatmap.
    /// </param>
    /// <param name="token">
    /// Cancellation token.
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
            Endpoint = Endpoints.BeatmapsetsEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Client = this
        }, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a user score on a beatmap from the API.
    /// </summary>
    /// <param name="beatmapId">
    /// If of the beatmap.
    /// </param>
    /// <param name="userId">
    /// Id of the user.
    /// </param>
    /// <param name="gameMode">
    /// Gamemode of the user. Defaults gamemode is picked when null.
    /// </param>
    /// <param name="mods">
    /// Mods to filter when looking for a score.
    /// </param>
    /// <param name="token">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Returns a <see cref="IBeatmapUserScore" />.
    /// </returns>
    public async Task<IBeatmapUserScore> GetUserBeatmapScoreAsync(
        long beatmapId,
        long userId,
        GameMode? gameMode = null,
        Mods? mods = null,
        CancellationToken token = default)
    {
        ThrowIfDisposed();
        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        Uri.TryCreate(
            string.Format(Endpoints.BeatmapsUserScoreEndpoint, beatmapId, userId),
            UriKind.Relative, out var uri);

        Dictionary<string, string> parameters = new();

        if (gameMode.HasValue)
        {
            parameters["mode"] = gameMode.Value.ToApiString();
        }

        if (mods.HasValue)
        {
            parameters["mods[]"] = mods.Value.ToApiString();
        }

        return await _handler.SendAsync<BeatmapUserScore, BeatmapUserScoreJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.BeatmapsBeatmapEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Parameters = parameters,
            Client = this
        }, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets all user scores on a beatmap from the API.
    /// </summary>
    /// <param name="beatmapId">
    /// If of the beatmap.
    /// </param>
    /// <param name="userId">
    /// Id of the user.
    /// </param>
    /// <param name="gameMode">
    /// Gamemode of the user. Defaults gamemode is picked when null.
    /// </param>
    /// <param name="token">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Returns a <see cref="IBeatmapUserScore" />.
    /// </returns>
    public async Task<IUserScores> GetUserBeatmapScoresAsync(long beatmapId, long userId, GameMode? gameMode = null, CancellationToken token = default)
    {
        ThrowIfDisposed();
        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        Uri.TryCreate(
            string.Format(Endpoints.BeatmapsUserScoresEndpoint, beatmapId, userId),
            UriKind.Relative, out var uri);

        Dictionary<string, string> parameters = new();

        if (gameMode.HasValue)
        {
            parameters["mode"] = gameMode.Value.ToApiString();
        }

        return await _handler.SendAsync<UserScores, UserScoresJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.BeatmapsBeatmapEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Parameters = parameters,
            Client = this
        }, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets the top scores on a beatmap from the API.
    /// </summary>
    /// <param name="beatmapId">
    /// If of the beatmap.
    /// </param>
    /// <param name="gameMode">
    /// Gamemode of the user. Defaults gamemode is picked when null.
    /// </param>
    /// <param name="mods">
    /// Mods to filter when looking for a score.
    /// </param>
    /// <param name="token">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Returns a <see cref="IBeatmapScores" />.
    /// </returns>
    public async Task<IBeatmapScores> GetBeatmapScoresAsync(
        long beatmapId,
        GameMode? gameMode = null,
        Mods? mods = null,
        CancellationToken token = default)
    {
        ThrowIfDisposed();
        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        Uri.TryCreate(
            string.Format(Endpoints.BeatmapsScoresEndpoint, beatmapId),
            UriKind.Relative, out var uri);

        Dictionary<string, string> parameters = new();

        if (gameMode.HasValue)
        {
            parameters["mode"] = gameMode.Value.ToApiString();
        }

        if (mods.HasValue)
        {
            parameters["mods[]"] = mods.Value.ToApiString();
        }

        return await _handler.SendAsync<BeatmapScores, BeatmapScoresJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.BeatmapsBeatmapEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Parameters = parameters,
            Client = this
        }, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a set of beatmaps by their ids. Up to 50 beatmaps can be requested at once.
    /// </summary>
    /// <param name="beatmapIds">
    /// Ids of the beatmaps.
    /// </param>
    /// <param name="token">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Returns a <see cref="IReadOnlyList{IBeatmap}" />
    /// </returns>
    public async Task<IReadOnlyList<IBeatmapCompact>> GetBeatmapsAsync(
        IReadOnlyList<long> beatmapIds,
        CancellationToken token = default)
    {
        ThrowIfDisposed();
        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        Uri.TryCreate(Endpoints.BeatmapsEndpoint, UriKind.Relative, out var uri);

        Dictionary<string, string> parameters = new();
        if (beatmapIds.Count > 0)
        {
            parameters["ids[]"] = string.Join("&ids[]=", beatmapIds);
        }

        return await _handler.SendMultipleAsync<BeatmapCompact, BeatmapJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.BeatmapsEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Parameters = parameters,
            Client = this
        }, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Lookups a beatmap by either its id, checksum or filename.
    /// </summary>
    /// <param name="id">
    /// Id of the beatmap.
    /// </param>
    /// <param name="checksum">
    /// Checksum of the beatmap.
    /// </param>
    /// <param name="filename">
    /// Filename of the beatmap.
    /// </param>
    /// <param name="token">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Returns a <see cref="IBeatmap"/>
    /// </returns>
    public async Task<IBeatmap> LookupBeatmapAsync(
        long? id = null,
        string? checksum = null,
        string? filename = null,
        CancellationToken token = default)
    {
        ThrowIfDisposed();

        if (!id.HasValue && checksum == null && filename == null)
        {
            throw new ArgumentException("One of the parameters is required for this endpoint.");
        }

        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        Uri.TryCreate(
            Endpoints.BeatmapsLookupEndpoint,
            UriKind.Relative, out var uri);

        Dictionary<string, string> parameters = new();

        if (id.HasValue)
        {
            parameters["id"] = id.Value.ToString();
        }

        if (!string.IsNullOrWhiteSpace(checksum))
        {
            parameters["checksum"] = checksum;
        }

        if (!string.IsNullOrWhiteSpace(filename))
        {
            parameters["filename"] = filename;
        }

        var result = await _handler.SendAsync<Beatmap, BeatmapJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.BeatmapsBeatmapEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Parameters = parameters,
            Client = this
        }, token);

        return result;
    }

    /// <summary>
    /// Enumerates the available beatmapsets from the API.
    /// </summary>
    /// <param name="builder">
    /// Builder that filters the search.
    /// </param>
    /// <param name="sorting">
    /// Type of sorting for the beatmapsets enumeration.
    /// </param>
    /// <param name="token">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Returns an asynchronous enumeration of <see cref="IBeatmapset"/>
    /// </returns>
    public async IAsyncEnumerable<IBeatmapset> EnumerateBeatmapsetsAsync(
        BeatmapsetsLookupBuilder? builder = null,
        BeatmapSorting sorting = BeatmapSorting.Ranked_Desc,
        [EnumeratorCancellation] CancellationToken token = default)
    {
        ThrowIfDisposed();
        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        IDictionary<string, string> parameters = new Dictionary<string, string>();

        if (builder != null)
        {
            parameters = builder.Build();
        }

        parameters["sort"] = sorting.ToString().ToLower();

        Uri.TryCreate(
            Endpoints.BeatmapsetsSearchEndpoint,
            UriKind.Relative, out var uri);

        var resultCount = long.MaxValue;
        var expectedCount = long.MaxValue;
        long totalCount = 0;

        while (resultCount > 0 && expectedCount != totalCount)
        {
            var result = await _handler.SendAsync<BeatmapsetSearchEnumeration, BeatmapsetSearchEnumerationJsonModel>(new OsuApiRequest
            {
                Endpoint = Endpoints.BeatmapsetsEndpoint,
                Method = HttpMethod.Get,
                Route = uri!,
                Token = _credentials,
                Parameters = parameters,
                Client = this
            }, token).ConfigureAwait(false);

            parameters["sort"] = result.Search.Sort.ToString().ToLower();

            expectedCount = result.Total;
            totalCount += result.Beatmapsets.Count;
            resultCount = result.Beatmapsets.Count;

            if (resultCount > 0)
            {
                foreach (var beatmapset in result.Beatmapsets)
                {
                    yield return beatmapset;
                }

                var lastBeatmapset = result.Beatmapsets[^1];

                switch (result.Search.Sort)
                {
                    case BeatmapSorting.Title_Desc:
                    case BeatmapSorting.Title_Asc:
                        parameters["cursor[approved_date]"] = lastBeatmapset.Title;
                        break;
                    case BeatmapSorting.Artist_Desc:
                    case BeatmapSorting.Artist_Asc:
                        parameters["cursor[approved_date]"] = lastBeatmapset.Artist;
                        break;
                    case BeatmapSorting.Difficulty_Desc:
                    case BeatmapSorting.Difficulty_Asc:
                        parameters["cursor[beatmaps.difficultyrating]"] = lastBeatmapset.Beatmaps[0].DifficultyRating.ToString(CultureInfo.InvariantCulture);
                        break;
                    case BeatmapSorting.Ranked_Desc:
                    case BeatmapSorting.Ranked_Asc:
                        if (lastBeatmapset.RankedDate.HasValue)
                        {
                            parameters["cursor[approved_date]"] = lastBeatmapset.RankedDate!.Value.ToUnixTimeMilliseconds().ToString();
                        }
                        else
                        {
                            parameters["cursor[approved_date]"] = "0";
                        }
                        break;
                    case BeatmapSorting.Updated_Desc:
                    case BeatmapSorting.Updated_Asc:
                        parameters["cursor[last_update]"] = lastBeatmapset.LastUpdated.ToUnixTimeMilliseconds().ToString();
                        break;
                    case BeatmapSorting.Rating_Desc:
                    case BeatmapSorting.Rating_Asc:
                        if (lastBeatmapset.Rating is not null)
                        {
                            parameters["cursor[rating]"] = lastBeatmapset.Rating!.ToString()!;
                        }
                        else
                        {
                            parameters["cursor[rating]"] = "0";
                        }
                        break;
                    case BeatmapSorting.Plays_Desc:
                    case BeatmapSorting.Plays_Asc:
                        parameters["cursor[play_count]"] = lastBeatmapset.PlayCount.ToString();
                        break;
                }

                parameters["cursor[id]"] = lastBeatmapset.Id.ToString();
            }
        }
    }

    /// <summary>
    /// Gets the current seasonal backgrounds from the API.
    /// </summary>
    /// <param name="token">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Returns a <see cref="ISeasonalBackgrounds"/>
    /// </returns>
    /// <remarks>
    /// Doesn't require any API authentication.
    /// </remarks>
    public async Task<ISeasonalBackgrounds> GetSeasonalBackgroundsAsync(
        CancellationToken token = default)
    {
        ThrowIfDisposed();

        Uri.TryCreate(
            Endpoints.SeasonalBackgroundsEndpoint,
            UriKind.Relative, out var uri);

        return await _handler.SendAsync<SeasonalBackgrounds, SeasonalBackgroundsJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.SeasonalBackgroundsEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Client = this
        }, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a score by its ID from the API.
    /// </summary>
    /// <param name="scoreId">
    /// Id of the score
    /// </param>
    /// <param name="gameMode">
    /// Game mode the score was playing in.
    /// </param>
    /// <param name="token">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Returns a <see cref="IScore"/>
    /// </returns>
    public async Task<IScore> GetScoreAsync(
        long scoreId,
        GameMode gameMode = GameMode.Osu,
        CancellationToken token = default)
    {
        ThrowIfDisposed();
        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        Uri.TryCreate(
            string.Format(Endpoints.ScoresEndpoint, gameMode.ToApiString(), scoreId),
            UriKind.Relative, out var uri);

        return await _handler.SendAsync<Score, ScoreJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.ScoresEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Client = this
        }, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets a replay by its score ID from the API.
    /// </summary>
    /// <param name="scoreId">
    /// Id of the score
    /// </param>
    /// <param name="gameMode">
    /// Game mode the score was playing in.
    /// </param>
    /// <param name="token">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Returns a <see cref="IScore"/>
    /// </returns>
    public async Task<IReplay> GetReplayAsync(
        long scoreId,
        GameMode gameMode = GameMode.Osu,
        CancellationToken token = default)
    {
        ThrowIfDisposed();
        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        Uri.TryCreate(
            string.Format(Endpoints.ScoresDownloadEndpoint, gameMode.ToApiString(), scoreId),
            UriKind.Relative, out var uri);

        var b64EncodedStream = await _handler.GetStreamAsync(new OsuApiRequest
        {
            Endpoint = Endpoints.ScoresEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Client = this
        }, token).ConfigureAwait(false);

        return Replay.FromStream(b64EncodedStream);
    }

    /// <summary>
    /// Gets spotlight rankings from the API.
    /// </summary>
    /// <param name="gameMode">The game mode to fetch rankings from.</param>
    /// <param name="page">(Optional) The page to fetch rankings from. Defaults to the first page.</param>
    /// <param name="filter">(Optional) Filter by results by all or by friends. Defaults to all.</param>
    /// <param name="spotlightId">(Optional) The spotlight ID. Defaults to the latest spotlight.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>Returns an <see cref="IRankingSpotlight"/> instance.</returns>
    public async Task<IRankingSpotlight> GetSpotlightRankingsAsync(
        GameMode gameMode,
        int? page = null,
        RankingFilter? filter = null,
        int? spotlightId = null,
        CancellationToken token = default)
    {
        ThrowIfDisposed();
        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        Uri.TryCreate(
            string.Format(Endpoints.RankingsEndpoint, gameMode.ToApiString(), RankingType.Spotlight.ToApiString()),
            UriKind.Relative, out var uri);

        IDictionary<string, string> parameters = new Dictionary<string, string>();

        if (page.HasValue)
            parameters["cursor[page]"] = page.Value.ToString();

        if (filter.HasValue)
            parameters["filter"] = filter.Value.ToApiString();

        if (spotlightId.HasValue)
            parameters["spotlight"] = spotlightId.Value.ToString();

        return await _handler.SendAsync<RankingSpotlight, RankingSpotlightJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.RankingsEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Parameters = parameters,
            Client = this
        }, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets country rankings from the API.
    /// </summary>
    /// <param name="gameMode">The game mode to fetch rankings from.</param>
    /// <param name="page">(Optional) The page to fetch rankings from. Defaults to the first page.</param>
    /// <param name="filter">(Optional) Filter by results by all or by friends. Defaults to all.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>Returns an <see cref="IRankingCountry"/> instance.</returns>
    public async Task<IRankingCountry> GetCountryRankingsAsync(
        GameMode gameMode,
        int? page = null,
        RankingFilter? filter = null,
        CancellationToken token = default)
    {
        ThrowIfDisposed();
        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        Uri.TryCreate(
            string.Format(Endpoints.RankingsEndpoint, gameMode.ToApiString(), RankingType.Country.ToApiString()),
            UriKind.Relative, out var uri);

        IDictionary<string, string> parameters = new Dictionary<string, string>();

        if (page.HasValue)
            parameters["cursor[page]"] = page.Value.ToString();

        if (filter.HasValue)
            parameters["filter"] = filter.Value.ToApiString();

        return await _handler.SendAsync<RankingCountry, RankingCountryJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.RankingsEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Parameters = parameters,
            Client = this
        }, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets performance rankings from the API.
    /// </summary>
    /// <param name="gameMode">The game mode to fetch rankings from.</param>
    /// <param name="page">(Optional) The page to fetch rankings from. Defaults to the first page.</param>
    /// <param name="countryCode">(Optional) The country to fetch rankings from. Defaults to none.</param>
    /// <param name="filter">(Optional) Filter by results by all or by friends. Defaults to all.</param>
    /// <param name="variant">(Optional) Filter by a mode-specific variant. Defaults to none.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>Returns an <see cref="IRankingPerformance"/> instance.</returns>
    public async Task<IRankingPerformance> GetPerformanceRankingsAsync(
        GameMode gameMode,
        string? countryCode = null,
        int? page = null,
        RankingFilter? filter = null,
        RankingVariant? variant = null,
        CancellationToken token = default)
    {
        ThrowIfDisposed();
        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        Uri.TryCreate(
            string.Format(Endpoints.RankingsEndpoint, gameMode.ToApiString(), RankingType.Performance.ToApiString()),
            UriKind.Relative, out var uri);

        IDictionary<string, string> parameters = new Dictionary<string, string>();

        if (!string.IsNullOrWhiteSpace(countryCode))
            parameters["country"] = countryCode;

        if (page.HasValue)
            parameters["cursor[page]"] = page.Value.ToString();

        if (filter.HasValue)
            parameters["filter"] = filter.Value.ToApiString();

        if (variant.HasValue)
            parameters["variant"] = variant.Value.ToApiString();

        return await _handler.SendAsync<RankingPerformance, RankingPerformanceJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.RankingsEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Parameters = parameters,
            Client = this
        }, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets score rankings from the API.
    /// </summary>
    /// <param name="gameMode">The game mode to fetch rankings from.</param>
    /// <param name="page">(Optional) The page to fetch rankings from. Defaults to the first page.</param>
    /// <param name="filter">(Optional) Filter by results by all or by friends. Defaults to all.</param>
    /// <param name="token">The cancellation token.</param>
    /// <returns>Returns an <see cref="IRankingScore"/> instance.</returns>
    public async Task<IRankingScore> GetScoreRankingsAsync(
        GameMode gameMode,
        int? page = null,
        RankingFilter? filter = null,
        CancellationToken token = default)
    {
        ThrowIfDisposed();
        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        Uri.TryCreate(
            string.Format(Endpoints.RankingsEndpoint, gameMode.ToApiString(), RankingType.Score.ToApiString()),
            UriKind.Relative, out var uri);

        IDictionary<string, string> parameters = new Dictionary<string, string>();

        if (page.HasValue)
            parameters["cursor[page]"] = page.Value.ToString();

        if (filter.HasValue)
            parameters["filter"] = filter.Value.ToApiString();

        return await _handler.SendAsync<RankingScore, RankingScoreJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.RankingsEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Parameters = parameters,
            Client = this
        }, token).ConfigureAwait(false);
    }

    /// <summary>
    /// Gets the beatmap difficulty attributes from the API.
    /// </summary>
    /// <param name="beatmapId">
    /// The id of the beatmap.
    /// </param>
    /// <param name="mods">
    /// The play mods used.
    /// </param>
    /// <param name="gameMode">
    /// The game mode used.
    /// </param>
    /// <param name="token">
    /// Cancellation token.
    /// </param>
    /// <returns>
    /// Returns a <see cref="IBeatmapDifficulty"/>.
    /// </returns>
    public async Task<IBeatmapDifficulty> GetBeatmapAttributesAsync(long beatmapId, Mods? mods = null, GameMode? gameMode = null, CancellationToken token = default)
    {
        ThrowIfDisposed();
        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        Uri.TryCreate(
            string.Format(Endpoints.BeatmapsBeatmapAttributesEndpoint, beatmapId),
            UriKind.Relative, out var uri);

        IDictionary<string, string> parameters = new Dictionary<string, string>();

        if (mods.HasValue)
        {
            parameters["mods"] = mods.ToApiString();
        }

        if (gameMode.HasValue)
        {
            parameters["ruleset"] = gameMode.ToApiString();
        }

        return await _handler.SendAsync<BeatmapDifficulty, BeatmapDifficultyJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.BeatmapsBeatmapEndpoint,
            Method = HttpMethod.Post,
            Route = uri!,
            Token = _credentials,
            Parameters = parameters,
            Client = this
        }, token).ConfigureAwait(false);
    }

    public async Task<IMultiplayerHistoryPage> GetMultiplayerHistoryPageAsync(int? after, bool ascending = true, CancellationToken token = default)
    {
        ThrowIfDisposed();
        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        Uri.TryCreate(Endpoints.MatchesHistoryEndpoint, UriKind.Relative, out var uri);
        IDictionary<string, string> parameters = new Dictionary<string, string>();
        if (after.HasValue)
        {
            parameters["cursor[match_id]"] = after.Value.ToString();
        }

        parameters["sort"] = ascending ? "id_asc" : "id_desc"; //TODO: add more sorting keys for GetUndocumentedMultiplayerHistoryPage

        return await _handler.SendAsync<MultiplayerHistoryPage, MultiplayerHistoryPageJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.MatchesHistoryEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Parameters = parameters,
            Client = this
        }, token).ConfigureAwait(false);
    }

    public async Task<IMultiplayerMatch> GetMultiplayerMatchAsync(long matchId, int? beforeEvent = null, CancellationToken token = default)
    {
        ThrowIfDisposed();
        await GetOrUpdateAccessTokenAsync(token).ConfigureAwait(false);

        Uri.TryCreate(
            string.Format(Endpoints.MatchesEndpoint, matchId),
            UriKind.Relative, out var uri);
        IDictionary<string, string> parameters = new Dictionary<string, string>();
        if (beforeEvent.HasValue)
        {
            parameters["before"] = beforeEvent.Value.ToString();
        }

        return await _handler.SendAsync<MultiplayerMatch, MultiplayerMatchJsonModel>(new OsuApiRequest
        {
            Endpoint = Endpoints.MatchesEndpoint,
            Method = HttpMethod.Get,
            Route = uri!,
            Token = _credentials,
            Parameters = parameters,
            Client = this
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