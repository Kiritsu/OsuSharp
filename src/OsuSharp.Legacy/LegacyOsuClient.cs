using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OsuSharp.Legacy.Entities;
using OsuSharp.Legacy.Enums;

namespace OsuSharp.Legacy;

public sealed class LegacyOsuClient
{
    #region Endpoints
    private string Root => LegacyOsuSharpConfiguration.BaseUrl;
    private const string Beatmaps = "/get_beatmaps";
    private const string Scores = "/get_scores";
    private const string User = "/get_user";
    private const string UserBest = "/get_user_best";
    private const string UserRecent = "/get_user_recent";
    private const string Match = "/get_match";
    private const string Replay = "/get_replay";
    #endregion

    internal LegacyOsuSharpConfiguration LegacyOsuSharpConfiguration { get; }

    private RateLimiter RateLimiter { get; }

    private RateLimiter ReplayRateLimiter { get; }

    /// <summary>
    ///     Represents the logger used to send log messages to the client.
    /// </summary>
    public OsuSharpLogger Logger { get; }

    private readonly JsonSerializerSettings _jsonSettings = new()
    {
        NullValueHandling = NullValueHandling.Ignore
    };

    #region Constructors
    /// <summary>
    ///     Initializes a new instance of <see cref="LegacyOsuClient"/> with the given configuration and the default configuration for the rate limiter.
    /// </summary>
    /// <param name="legacyOsuSharpConfiguration">Configuration to use for this instance.</param>
    public LegacyOsuClient(LegacyOsuSharpConfiguration legacyOsuSharpConfiguration) : this(legacyOsuSharpConfiguration, new RateLimiterConfiguration())
    {

    }

    /// <summary>
    ///     Initializes a new instance of <see cref="LegacyOsuClient"/> with the given configuration and the one for the rate limiter.
    /// </summary>
    /// <param name="legacyOsuSharpConfiguration">Configuration to use for this instance.</param>
    /// <param name="rateLimiterConfiguration">Rate limiting configuration.</param>
    public LegacyOsuClient(LegacyOsuSharpConfiguration legacyOsuSharpConfiguration, RateLimiterConfiguration rateLimiterConfiguration)
    {
        LegacyOsuSharpConfiguration = legacyOsuSharpConfiguration;
        RateLimiter = new RateLimiter(rateLimiterConfiguration);
        ReplayRateLimiter = new RateLimiter(new RateLimiterConfiguration
        {
            MaxRequest = 10,
            ThrowOnRatelimitHit = rateLimiterConfiguration.ThrowOnRatelimitHit
        });

        if (string.IsNullOrWhiteSpace(LegacyOsuSharpConfiguration.ApiKey))
        {
            throw new OsuSharpException("The given api key is not valid.", HttpStatusCode.Unauthorized);
        }

        LegacyOsuSharpConfiguration.HttpClient ??= new HttpClient();

        Logger = new OsuSharpLogger();
    }
    #endregion

    #region Beatmap

    /// <summary>
    ///     Gets lasts published beatmaps.
    /// </summary>
    /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns><see cref="IReadOnlyList{T}"/></returns>
    public async Task<IReadOnlyList<LegacyBeatmap>> GetBeatmapsAsync(int limit = 500, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["limit"] = limit
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        foreach (var beatmap in request)
        {
            beatmap.Client = this;
        }
        return request;
    }

    /// <summary>
    ///     Gets lasts published beatmaps since a specific <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="since">Start <see cref="DateTimeOffset"/> reference.</param>
    /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyBeatmap>> GetBeatmapsAsync(DateTimeOffset since, int limit = 500, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["limit"] = limit,
            ["since"] = since.ToString("yyyy-MM-dd HH:mm:ss")
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        foreach (var beatmap in request)
        {
            beatmap.Client = this;
        }
        return request;
    }

    /// <summary>
    ///     Gets lasts published beatmaps on a specific <see cref="GameMode"/>.
    /// </summary>
    /// <param name="gameMode">Game mode of the beatmaps.</param>
    /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
    /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns><see cref="IReadOnlyList{Beatmap}"/></returns>
    public async Task<IReadOnlyList<LegacyBeatmap>> GetBeatmapsAsync(GameMode gameMode, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["limit"] = limit,
            ["a"] = includeConvertedBeatmaps ? 1 : 0,
            ["m"] = (int)gameMode
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        foreach (var beatmap in request)
        {
            beatmap.Client = this;
        }
        return request;
    }

    /// <summary>
    ///     Gets lasts published beatmaps since a specific <see cref="DateTimeOffset"/> on a specific <see cref="GameMode"/>.
    /// </summary>
    /// <param name="since">Start <see cref="DateTimeOffset"/> reference.</param>
    /// <param name="gameMode">Game mode of the beatmaps.</param>
    /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
    /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyBeatmap>> GetBeatmapsAsync(DateTimeOffset since, GameMode gameMode, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["limit"] = limit,
            ["since"] = since.ToString("yyyy-MM-dd HH:mm:ss"),
            ["a"] = includeConvertedBeatmaps ? 1 : 0,
            ["m"] = (int)gameMode
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        foreach (var beatmap in request)
        {
            beatmap.Client = this;
        }
        return request;
    }

    /// <summary>
    ///     Gets lasts beatmaps published by the given author id since a specific <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="authorId">Id of the author.</param>
    /// <param name="since">Start <see cref="DateTimeOffset"/> reference.</param>
    /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
    /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyBeatmap>> GetBeatmapsByAuthorIdAsync(long authorId, DateTimeOffset since, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["limit"] = limit,
            ["since"] = since.ToString("yyyy-MM-dd HH:mm:ss"),
            ["a"] = includeConvertedBeatmaps ? 1 : 0,
            ["type"] = "id",
            ["u"] = authorId
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        foreach (var beatmap in request)
        {
            beatmap.Client = this;
        }
        return request;
    }

    /// <summary>
    ///     Gets lasts beatmaps published by the given author id since a specific <see cref="DateTimeOffset"/> on a specific <see cref="GameMode"/>.
    /// </summary>
    /// <param name="authorId">Id of the author.</param>
    /// <param name="since">Start <see cref="DateTimeOffset"/> reference.</param>
    /// <param name="gameMode">Game mode of the beatmaps.</param>
    /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
    /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyBeatmap>> GetBeatmapsByAuthorIdAsync(long authorId, DateTimeOffset since, GameMode gameMode, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["limit"] = limit,
            ["since"] = since.ToString("yyyy-MM-dd HH:mm:ss"),
            ["a"] = includeConvertedBeatmaps ? 1 : 0,
            ["type"] = "id",
            ["u"] = authorId,
            ["m"] = (int)gameMode
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        foreach (var beatmap in request)
        {
            beatmap.Client = this;
        }
        return request;
    }

    /// <summary>
    ///     Gets lasts beatmaps published by the given author id.
    /// </summary>
    /// <param name="authorId">Id of the author.</param>
    /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
    /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyBeatmap>> GetBeatmapsByAuthorIdAsync(long authorId, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["limit"] = limit,
            ["a"] = includeConvertedBeatmaps ? 1 : 0,
            ["type"] = "id",
            ["u"] = authorId
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        foreach (var beatmap in request)
        {
            beatmap.Client = this;
        }
        return request;
    }

    /// <summary>
    ///     Gets lasts beatmaps published by the given author id on a specific <see cref="GameMode"/>.
    /// </summary>
    /// <param name="authorId">Id of the author.</param>
    /// <param name="gameMode">Game mode of the beatmaps.</param>
    /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
    /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyBeatmap>> GetBeatmapsByAuthorIdAsync(long authorId, GameMode gameMode, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["limit"] = limit,
            ["a"] = includeConvertedBeatmaps ? 1 : 0,
            ["type"] = "id",
            ["u"] = authorId,
            ["m"] = (int)gameMode
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        foreach (var beatmap in request)
        {
            beatmap.Client = this;
        }
        return request;
    }

    /// <summary>
    ///     Gets lasts beatmaps published by the given author username since a specific <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="username">Username of the author.</param>
    /// <param name="since">Start <see cref="DateTimeOffset"/> reference.</param>
    /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
    /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyBeatmap>> GetBeatmapsByAuthorUsernameAsync(string username, DateTimeOffset since, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["limit"] = limit,
            ["since"] = since.ToString("yyyy-MM-dd HH:mm:ss"),
            ["a"] = includeConvertedBeatmaps ? 1 : 0,
            ["type"] = "string",
            ["u"] = username
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        foreach (var beatmap in request)
        {
            beatmap.Client = this;
        }
        return request;
    }

    /// <summary>
    ///     Gets lasts beatmaps published by the given author username since a specific <see cref="DateTimeOffset"/> on a specific <see cref="GameMode"/>.
    /// </summary>
    /// <param name="username">Username of the author.</param>
    /// <param name="since">Start <see cref="DateTimeOffset"/> reference.</param>
    /// <param name="gameMode">Game mode of the beatmaps.</param>
    /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
    /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyBeatmap>> GetBeatmapsByAuthorUsernameAsync(string username, DateTimeOffset since, GameMode gameMode, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["limit"] = limit,
            ["since"] = since.ToString("yyyy-MM-dd HH:mm:ss"),
            ["a"] = includeConvertedBeatmaps ? 1 : 0,
            ["type"] = "string",
            ["u"] = username,
            ["m"] = (int)gameMode
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        foreach (var beatmap in request)
        {
            beatmap.Client = this;
        }
        return request;
    }

    /// <summary>
    ///     Gets lasts beatmaps published by the given author username.
    /// </summary>
    /// <param name="username">Username of the author.</param>
    /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
    /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyBeatmap>> GetBeatmapsByAuthorUsernameAsync(string username, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["limit"] = limit,
            ["a"] = includeConvertedBeatmaps ? 1 : 0,
            ["type"] = "string",
            ["u"] = username
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        foreach (var beatmap in request)
        {
            beatmap.Client = this;
        }
        return request;
    }

    /// <summary>
    ///     Gets lasts beatmaps published by the given author username on a specific <see cref="GameMode"/>.
    /// </summary>
    /// <param name="username">Username of the author.</param>
    /// <param name="gameMode">Game mode of the beatmaps.</param>
    /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
    /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyBeatmap>> GetBeatmapsByAuthorUsernameAsync(string username, GameMode gameMode, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["limit"] = limit,
            ["a"] = includeConvertedBeatmaps ? 1 : 0,
            ["type"] = "string",
            ["u"] = username,
            ["m"] = (int)gameMode
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        foreach (var beatmap in request)
        {
            beatmap.Client = this;
        }
        return request;
    }

    /// <summary>
    ///     Gets a set of beatmaps depending on the beatmapset id.
    /// </summary>
    /// <param name="beatmapsetId">Id of the beatmapset.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyBeatmap>> GetBeatmapsetAsync(long beatmapsetId, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["s"] = beatmapsetId
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        foreach (var beatmap in request)
        {
            beatmap.Client = this;
        }
        return request;
    }

    /// <summary>
    ///     Gets a set of beatmaps depending on the beatmapset id.
    /// </summary>
    /// <param name="beatmapsetId">Id of the beatmapset.</param>
    /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyBeatmap>> GetBeatmapsetAsync(long beatmapsetId, bool includeConvertedBeatmaps, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["s"] = beatmapsetId,
            ["a"] = includeConvertedBeatmaps ? 1 : 0
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        foreach (var beatmap in request)
        {
            beatmap.Client = this;
        }
        return request;
    }

    /// <summary>
    ///      Gets a set of beatmaps on a specific <see cref="GameMode"/> depending on the beatmapset id.
    /// </summary>
    /// <param name="beatmapsetId">Id of the beatmapset.</param>
    /// <param name="gameMode">Game mode of the beatmaps.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyBeatmap>> GetBeatmapsetAsync(long beatmapsetId, GameMode gameMode, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["m"] = (int)gameMode,
            ["s"] = beatmapsetId
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        foreach (var beatmap in request)
        {
            beatmap.Client = this;
        }
        return request;
    }

    /// <summary>
    ///      Gets a set of beatmaps on a specific <see cref="GameMode"/> depending on the beatmapset id.
    /// </summary>
    /// <param name="beatmapsetId">Id of the beatmapset.</param>
    /// <param name="gameMode">Game mode of the beatmaps.</param>
    /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyBeatmap>> GetBeatmapsetAsync(long beatmapsetId, GameMode gameMode, bool includeConvertedBeatmaps, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["m"] = (int)gameMode,
            ["s"] = beatmapsetId,
            ["a"] = includeConvertedBeatmaps ? 1 : 0
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        foreach (var beatmap in request)
        {
            beatmap.Client = this;
        }
        return request;
    }

    /// <summary>
    ///     Gets a beatmap by it's id.
    /// </summary>
    /// <param name="beatmapId">Id of the beatmap.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<LegacyBeatmap> GetBeatmapByIdAsync(long beatmapId, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["b"] = beatmapId
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        if (request.Count > 0)
        {
            request[0].Client = this;
            return request[0];
        }
        return null;
    }

    /// <summary>
    ///     Gets a beatmap by it's id.
    /// </summary>
    /// <param name="beatmapId">Id of the beatmap.</param>
    /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<LegacyBeatmap> GetBeatmapByIdAsync(long beatmapId, bool includeConvertedBeatmaps, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["b"] = beatmapId,
            ["a"] = includeConvertedBeatmaps ? 1 : 0
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        if (request.Count > 0)
        {
            request[0].Client = this;
            return request[0];
        }
        return null;
    }

    /// <summary>
    ///     Gets a beatmap by it's id.
    /// </summary>
    /// <param name="beatmapId">Id of the beatmap.</param>
    /// <param name="gameMode">Game mode.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<LegacyBeatmap> GetBeatmapByIdAsync(long beatmapId, GameMode gameMode, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["m"] = (int)gameMode,
            ["b"] = beatmapId
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        if (request.Count > 0)
        {
            request[0].Client = this;
            return request[0];
        }
        return null;
    }

    /// <summary>
    ///     Gets a beatmap by it's id.
    /// </summary>
    /// <param name="beatmapId">Id of the beatmap.</param>
    /// <param name="gameMode">Game mode.</param>
    /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<LegacyBeatmap> GetBeatmapByIdAsync(long beatmapId, GameMode gameMode, bool includeConvertedBeatmaps, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["m"] = (int)gameMode,
            ["b"] = beatmapId,
            ["a"] = includeConvertedBeatmaps ? 1 : 0
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        if (request.Count > 0)
        {
            request[0].Client = this;
            return request[0];
        }
        return null;
    }

    /// <summary>
    ///     Gets a beatmap corresponding to the given replay hash.
    /// </summary>
    /// <param name="hash">Hash of a replay.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<LegacyBeatmap> GetBeatmapByHashAsync(string hash, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["h"] = hash
        };

        var request = await RequestAsync<IReadOnlyList<LegacyBeatmap>>(Beatmaps, dict, token).ConfigureAwait(false);
        if (request.Count > 0)
        {
            request[0].Client = this;
            return request[0];
        }
        return null;
    }

    #endregion

    #region User
    /// <summary>
    ///     Get a user by its id.
    /// </summary>
    /// <param name="userId">Id of the user.</param>
    /// <param name="gameMode">Game mode to return metadata from.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<LegacyUser> GetUserByUserIdAsync(long userId, GameMode gameMode, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["u"] = userId,
            ["m"] = (int)gameMode,
            ["type"] = "id",
            ["event_days"] = 1
        };

        var request = await RequestAsync<IReadOnlyList<LegacyUser>>(User, dict, token).ConfigureAwait(false);
        if (request.Count > 0)
        {
            request[0].Client = this;
            request[0].GameMode = gameMode;
            return request[0];
        }

        return null;
    }

    /// <summary>
    ///     Get a user by its id.
    /// </summary>
    /// <param name="userId">Id of the user.</param>
    /// <param name="gameMode">Game mode to return metadata from.</param>
    /// <param name="eventDays">Gets the max amount of day between now and last event date. [1;31]</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<LegacyUser> GetUserByUserIdAsync(long userId, GameMode gameMode, int eventDays, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["u"] = userId,
            ["m"] = (int)gameMode,
            ["type"] = "id",
            ["event_days"] = eventDays
        };

        var request = await RequestAsync<IReadOnlyList<LegacyUser>>(User, dict, token).ConfigureAwait(false);
        if (request.Count > 0)
        {
            request[0].Client = this;
            request[0].GameMode = gameMode;
            return request[0];
        }

        return null;
    }

    /// <summary>
    ///     Get a user by its id.
    /// </summary>
    /// <param name="username">Username of the user.</param>
    /// <param name="gameMode">Game mode to return metadata from.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<LegacyUser> GetUserByUsernameAsync(string username, GameMode gameMode, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["u"] = username,
            ["m"] = (int)gameMode,
            ["type"] = "string",
            ["event_days"] = 1
        };

        var request = await RequestAsync<IReadOnlyList<LegacyUser>>(User, dict, token).ConfigureAwait(false);
        if (request.Count > 0)
        {
            request[0].Client = this;
            request[0].GameMode = gameMode;
            return request[0];
        }

        return null;
    }

    /// <summary>
    ///     Get a user by its id.
    /// </summary>
    /// <param name="username">Username of the user.</param>
    /// <param name="gameMode">Game mode to return metadata from.</param>
    /// <param name="eventDays">Gets the max amount of day between now and last event date. [1;31]</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<LegacyUser> GetUserByUsernameAsync(string username, GameMode gameMode, int eventDays, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["u"] = username,
            ["m"] = (int)gameMode,
            ["type"] = "string",
            ["event_days"] = eventDays
        };

        var request = await RequestAsync<IReadOnlyList<LegacyUser>>(User, dict, token).ConfigureAwait(false);
        if (request.Count > 0)
        {
            request[0].Client = this;
            request[0].GameMode = gameMode;
            return request[0];
        }

        return null;
    }
    #endregion

    #region Score
    /// <summary>
    ///     Gets a set of scores by a beatmap id.
    /// </summary>
    /// <param name="beatmapId">Id of the beatmap.</param>
    /// <param name="gameMode">Game mode to fetch the scores from.</param>
    /// <param name="limit">Limit [1; 100] of scores to return.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyScore>> GetScoresByBeatmapId(long beatmapId, GameMode gameMode, int limit = 100, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["b"] = beatmapId,
            ["m"] = (int)gameMode,
            ["limit"] = limit
        };

        var request = await RequestAsync<IReadOnlyList<LegacyScore>>(Scores, dict, token).ConfigureAwait(false);
        foreach (var score in request)
        {
            score.Client = this;
            score.GameMode = gameMode;
            score.BeatmapId = beatmapId;
        }
        return request;
    }

    /// <summary>
    ///     Gets a set of scores by a beatmap id.
    /// </summary>
    /// <param name="beatmapId">Id of the beatmap.</param>
    /// <param name="gameMode">Game mode to fetch the scores from.</param>
    /// <param name="enabledMods">Mods to fetch the scores from.</param>
    /// <param name="limit">Limit [1; 100] of scores to return.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyScore>> GetScoresByBeatmapId(long beatmapId, GameMode gameMode, Mode enabledMods, int limit = 100, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["b"] = beatmapId,
            ["m"] = (int)gameMode,
            ["mods"] = (int)enabledMods,
            ["limit"] = limit
        };

        var request = await RequestAsync<IReadOnlyList<LegacyScore>>(Scores, dict, token).ConfigureAwait(false);
        foreach (var score in request)
        {
            score.Client = this;
            score.GameMode = gameMode;
            score.BeatmapId = beatmapId;
        }
        return request;
    }

    /// <summary>
    ///     Gets a set of scores by a beatmap id.
    /// </summary>
    /// <param name="beatmapId">Id of the beatmap.</param>
    /// <param name="userId">Id of the user</param>
    /// <param name="gameMode">Game mode to fetch the scores from.</param>
    /// <param name="limit">Limit [1; 100] of scores to return.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyScore>> GetScoresByBeatmapIdAndUserIdAsync(long beatmapId, long userId, GameMode gameMode, int limit = 100, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["b"] = beatmapId,
            ["u"] = userId,
            ["m"] = (int)gameMode,
            ["type"] = "id",
            ["limit"] = limit
        };

        var request = await RequestAsync<IReadOnlyList<LegacyScore>>(Scores, dict, token).ConfigureAwait(false);
        foreach (var score in request)
        {
            score.Client = this;
            score.GameMode = gameMode;
            score.BeatmapId = beatmapId;
            score.UserId = userId;
        }
        return request;
    }

    /// <summary>
    ///     Gets a set of scores by a beatmap id.
    /// </summary>
    /// <param name="beatmapId">Id of the beatmap.</param>
    /// <param name="userId">Id of the user</param>
    /// <param name="gameMode">Game mode to fetch the scores from.</param>
    /// <param name="enabledMods">Mods to fetch the scores from.</param>
    /// <param name="limit">Limit [1; 100] of scores to return.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyScore>> GetScoresByBeatmapIdAndUserIdAsync(long beatmapId, long userId, GameMode gameMode, Mode enabledMods, int limit = 100, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["b"] = beatmapId,
            ["u"] = userId,
            ["m"] = (int)gameMode,
            ["mods"] = (int)enabledMods,
            ["type"] = "id",
            ["limit"] = limit
        };

        var request = await RequestAsync<IReadOnlyList<LegacyScore>>(Scores, dict, token).ConfigureAwait(false);
        foreach (var score in request)
        {
            score.Client = this;
            score.GameMode = gameMode;
            score.BeatmapId = beatmapId;
            score.UserId = userId;
        }
        return request;
    }

    /// <summary>
    ///     Gets a set of scores by a beatmap id.
    /// </summary>
    /// <param name="beatmapId">Id of the beatmap.</param>
    /// <param name="username">Username of the user</param>
    /// <param name="gameMode">Game mode to fetch the scores from.</param>
    /// <param name="limit">Limit [1; 100] of scores to return.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyScore>> GetScoresByBeatmapIdAndUsernameAsync(long beatmapId, string username, GameMode gameMode, int limit = 100, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["b"] = beatmapId,
            ["u"] = username,
            ["m"] = (int)gameMode,
            ["type"] = "string",
            ["limit"] = limit
        };

        var request = await RequestAsync<IReadOnlyList<LegacyScore>>(Scores, dict, token).ConfigureAwait(false);
        foreach (var score in request)
        {
            score.Client = this;
            score.GameMode = gameMode;
            score.BeatmapId = beatmapId;
            score.Username = username;
        }
        return request;
    }

    /// <summary>
    ///     Gets a set of scores by a beatmap id.
    /// </summary>
    /// <param name="beatmapId">Id of the beatmap.</param>
    /// <param name="username">Username of the user</param>
    /// <param name="gameMode">Game mode to fetch the scores from.</param>
    /// <param name="enabledMods">Mods to fetch the scores from.</param>
    /// <param name="limit">Limit [1; 100] of scores to return.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyScore>> GetScoresByBeatmapIdAndUsernameAsync(long beatmapId, string username, GameMode gameMode, Mode enabledMods, int limit = 100, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["b"] = beatmapId,
            ["u"] = username,
            ["m"] = (int)gameMode,
            ["mods"] = (int)enabledMods,
            ["type"] = "string",
            ["limit"] = limit
        };

        var request = await RequestAsync<IReadOnlyList<LegacyScore>>(Scores, dict, token).ConfigureAwait(false);
        foreach (var score in request)
        {
            score.Client = this;
            score.GameMode = gameMode;
            score.BeatmapId = beatmapId;
            score.Username = username;
        }
        return request;
    }
    #endregion

    #region ScoreUserBest
    public async Task<IReadOnlyList<LegacyScore>> GetUserBestsByUserIdAsync(long userId, GameMode gameMode, int limit = 100, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["u"] = userId,
            ["m"] = (int)gameMode,
            ["limit"] = limit,
            ["type"] = "id"
        };

        var request = await RequestAsync<IReadOnlyList<LegacyScore>>(UserBest, dict, token).ConfigureAwait(false);
        foreach (var score in request)
        {
            score.Client = this;
            score.GameMode = gameMode;
            score.UserId = userId;
        }
        return request;
    }

    public async Task<IReadOnlyList<LegacyScore>> GetUserBestsByUsernameAsync(string username, GameMode gameMode, int limit = 100, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["u"] = username,
            ["m"] = (int)gameMode,
            ["limit"] = limit,
            ["type"] = "string"
        };

        var request = await RequestAsync<IReadOnlyList<LegacyScore>>(UserBest, dict, token).ConfigureAwait(false);
        foreach (var score in request)
        {
            score.Client = this;
            score.GameMode = gameMode;
            score.Username = username;
        }
        return request;
    }
    #endregion

    #region ScoreUserRecent
    public async Task<IReadOnlyList<LegacyScore>> GetUserRecentsByUserIdAsync(long userId, GameMode gameMode, int limit = 100, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["u"] = userId,
            ["m"] = (int)gameMode,
            ["limit"] = limit,
            ["type"] = "id"
        };

        var request = await RequestAsync<IReadOnlyList<LegacyScore>>(UserRecent, dict, token).ConfigureAwait(false);
        foreach (var score in request)
        {
            score.Client = this;
            score.GameMode = gameMode;
            score.UserId = userId;
        }
        return request;
    }

    public async Task<IReadOnlyList<LegacyScore>> GetUserRecentsByUsernameAsync(string username, GameMode gameMode, int limit = 100, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["u"] = username,
            ["m"] = (int)gameMode,
            ["limit"] = limit,
            ["type"] = "string"
        };

        var request = await RequestAsync<IReadOnlyList<LegacyScore>>(UserRecent, dict, token).ConfigureAwait(false);
        foreach (var score in request)
        {
            score.Client = this;
            score.GameMode = gameMode;
            score.Username = username;
        }
        return request;
    }
    #endregion

    #region Multiplayer
    /// <summary>
    ///     Get a multiplayer room informations.
    /// </summary>
    /// <param name="matchId">Id of the multiplayer room.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<LegacyMultiplayerRoom> GetMultiplayerRoomAsync(long matchId, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["mp"] = matchId
        };

        try
        {
            var request = await RequestAsync<LegacyMultiplayerRoom>(Match, dict, token).ConfigureAwait(false);
            request.Client = this;
            return request;
        }
        catch (JsonSerializationException)
        {
            return null;
        }
    }
    #endregion

    #region Replay

    /// <summary>
    ///     Gets a replay.
    /// </summary>
    /// <param name="beatmapId">Id of the beatmap.</param>
    /// <param name="userId">User id that played that beatmap.</param>
    /// <param name="gameMode">Game mode the play has been played in.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<LegacyReplay> GetReplayByUserIdAsync(long beatmapId, long userId, GameMode gameMode, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["b"] = beatmapId,
            ["u"] = userId,
            ["m"] = (int)gameMode,
            ["type"] = "id"
        };

        var request = await RequestAsync<LegacyReplay>(Replay, ReplayRateLimiter, dict, token).ConfigureAwait(false);
        request.Client = this;
        return request;
    }

    /// <summary>
    ///     Gets a replay.
    /// </summary>
    /// <param name="beatmapId">Id of the beatmap.</param>
    /// <param name="username">Username of the player.</param>
    /// <param name="gameMode">Game mode the play has been played in.</param>
    /// <param name="token">Cancellation token used to cancel the current request.</param>
    /// <returns></returns>
    public async Task<LegacyReplay> GetReplayByUsernameAsync(long beatmapId, string username, GameMode gameMode, CancellationToken token = default)
    {
        var dict = new Dictionary<string, object>
        {
            ["b"] = beatmapId,
            ["u"] = username,
            ["m"] = (int)gameMode,
            ["type"] = "string"
        };

        var request = await RequestAsync<LegacyReplay>(Replay, ReplayRateLimiter, dict, token).ConfigureAwait(false);
        request.Client = this;
        return request;
    }

    #endregion

    private Task<T> RequestAsync<T>(string endpoint, IReadOnlyDictionary<string, object> parameters = null, CancellationToken token = default)
    {
        return RequestAsync<T>(endpoint, RateLimiter, parameters, token);
    }

    private async Task<T> RequestAsync<T>(string endpoint, RateLimiter rateLimiter, IReadOnlyDictionary<string, object> parameters = null, CancellationToken token = default)
    {
        await rateLimiter.HandleAsync(token).ConfigureAwait(false);
        rateLimiter.IncrementRequestCount();

        var url = $"{Root}{endpoint}?k={LegacyOsuSharpConfiguration.ApiKey}";

        if (parameters is {Count: > 0})
        {
            var builder = new StringBuilder();
            foreach (var (key, value) in parameters)
            {
                builder.Append($"&{key}={value}");
            }

            url += builder.ToString();
        }

        var response = await LegacyOsuSharpConfiguration.HttpClient.GetAsync(url, token).ConfigureAwait(false);
        var message = await response.Content.ReadAsStringAsync(token).ConfigureAwait(false);

        if (response.StatusCode == HttpStatusCode.OK)
        {
            Logger.LogMessage($"Endpoint {endpoint} used: [{rateLimiter.RequestCount}/{rateLimiter.Configuration.MaxRequest}]: {string.Join(", ", parameters!.Select(x => x.Key + ":" + x.Value))}");
            return JsonConvert.DeserializeObject<T>(message, _jsonSettings);
        }

        throw new OsuSharpException(JObject.Parse(message)["error"]!.Value<string>(), response.StatusCode);
    }
}