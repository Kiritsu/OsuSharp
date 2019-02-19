using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OsuSharp.Entities;
using OsuSharp.Enums;

namespace OsuSharp
{
    public sealed class OsuApi
    {
        private const string Root = "https://osu.ppy.sh/api";

        private const string Beatmaps = "/get_beatmaps";

        private const string Scores = "/get_scores";

        private const string User = "/get_user";
        private const string UserBest = "/get_user_best";
        private const string UserRecent = "/get_user_recent";

        private const string Match = "/get_match";

        private const string Replay = "/get_replay";

        internal OsuSharpConfiguration OsuSharpConfiguration { get; }

        private RateLimiter RateLimiter { get; }

        private RateLimiter ReplayRateLimiter { get; }

        /// <summary>
        ///     Represents the logger used to send log messages to the client.
        /// </summary>
        public OsuSharpLogger Logger { get; }

        /// <summary>
        ///     Initializes a new instance of <see cref="OsuApi"/> with the given configuration and the default configuration for the rate limiter.
        /// </summary>
        /// <param name="osuSharpConfiguration">Configuration to use for this instance.</param>
        public OsuApi(OsuSharpConfiguration osuSharpConfiguration) : this(osuSharpConfiguration, new RateLimiterConfiguration())
        {

        }

        /// <summary>
        ///     Initializes a new instance of <see cref="OsuApi"/> with the given configuration and the one for the rate limiter.
        /// </summary>
        /// <param name="osuSharpConfiguration">Configuration to use for this instance.</param>
        /// <param name="rateLimiterConfiguration">Rate limiting configuration.</param>
        public OsuApi(OsuSharpConfiguration osuSharpConfiguration, RateLimiterConfiguration rateLimiterConfiguration)
        {
            OsuSharpConfiguration = osuSharpConfiguration;
            RateLimiter = new RateLimiter(rateLimiterConfiguration);
            ReplayRateLimiter = new RateLimiter(new RateLimiterConfiguration
            {
                MaxRequest = 10,
                ThrowOnRatelimitHit = rateLimiterConfiguration.ThrowOnRatelimitHit
            });

            if (string.IsNullOrWhiteSpace(OsuSharpConfiguration.ApiKey))
            {
                throw new OsuSharpException("The given api key is not valid.");
            }

            if (OsuSharpConfiguration.Client is null)
            {
                OsuSharpConfiguration.Client = new HttpClient();
            }

            Logger = new OsuSharpLogger();
        }

        #region Beatmap

        /// <summary>
        ///     Gets lasts published beatmaps.
        /// </summary>
        /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns><see cref="IReadOnlyList{Beatmap}"/></returns>
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsAsync(int limit = 500, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>
            {
                ["limit"] = limit
            };

            var request = await RequestAsync(Beatmaps, dict, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
        }

        /// <summary>
        ///     Gets lasts published beatmaps since a specific <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="since">Start <see cref="DateTimeOffset"> reference.</param>
        /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsAsync(DateTimeOffset since, int limit = 500, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["since"] = since.ToString("yyyy-MM-dd HH:mm:ss")
            };

            var request = await RequestAsync(Beatmaps, dict, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
        }

        /// <summary>
        ///     Gets lasts published beatmaps on a specific <see cref="GameMode"/>.
        /// </summary>
        /// <param name="gameMode">Game mode of the beatmaps.</param>
        /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
        /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns><see cref="IReadOnlyList{Beatmap}"/></returns>
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsAsync(GameMode gameMode = GameMode.Standard, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["a"] = includeConvertedBeatmaps ? 1 : 0,
                ["m"] = (int)gameMode
            };

            var request = await RequestAsync(Beatmaps, dict, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
        }

        /// <summary>
        ///     Gets lasts published beatmaps since a specific <see cref="DateTimeOffset"/> on a specific <see cref="GameMode"/>.
        /// </summary>
        /// <param name="since">Start <see cref="DateTimeOffset"> reference.</param>
        /// <param name="gameMode">Game mode of the beatmaps.</param>
        /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
        /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsAsync(DateTimeOffset since, GameMode gameMode = GameMode.Standard, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["since"] = since.ToString("yyyy-MM-dd HH:mm:ss"),
                ["a"] = includeConvertedBeatmaps ? 1 : 0,
                ["m"] = (int)gameMode
            };

            var request = await RequestAsync(Beatmaps, dict, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
        }

        /// <summary>
        ///     Gets lasts beatmaps published by the given author id since a specific <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="authorId">Id of the author.</param>
        /// <param name="since">Start <see cref="DateTimeOffset"> reference.</param>
        /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
        /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsByAuthorIdAsync(long authorId, DateTimeOffset since, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["since"] = since.ToString("yyyy-MM-dd HH:mm:ss"),
                ["a"] = includeConvertedBeatmaps ? 1 : 0,
                ["type"] = "id",
                ["u"] = authorId
            };

            var request = await RequestAsync(Beatmaps, dict, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
        }

        /// <summary>
        ///     Gets lasts beatmaps published by the given author id since a specific <see cref="DateTimeOffset"/> on a specific <see cref="GameMode"/>.
        /// </summary>
        /// <param name="authorId">Id of the author.</param>
        /// <param name="since">Start <see cref="DateTimeOffset"> reference.</param>
        /// <param name="gameMode">Game mode of the beatmaps.</param>
        /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
        /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsByAuthorIdAsync(long authorId, DateTimeOffset since, GameMode gameMode = GameMode.Standard, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
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

            var request = await RequestAsync(Beatmaps, dict, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
        }

        /// <summary>
        ///     Gets lasts beatmaps published by the given author id.
        /// </summary>
        /// <param name="authorId">Id of the author.</param>
        /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
        /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsByAuthorIdAsync(long authorId, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["a"] = includeConvertedBeatmaps ? 1 : 0,
                ["type"] = "id",
                ["u"] = authorId
            };

            var request = await RequestAsync(Beatmaps, dict, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
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
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsByAuthorIdAsync(long authorId, GameMode gameMode = GameMode.Standard, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["a"] = includeConvertedBeatmaps ? 1 : 0,
                ["type"] = "id",
                ["u"] = authorId,
                ["m"] = (int)gameMode
            };

            var request = await RequestAsync(Beatmaps, dict, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
        }

        /// <summary>
        ///     Gets lasts beatmaps published by the given author username since a specific <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="username">Username of the author.</param>
        /// <param name="since">Start <see cref="DateTimeOffset"> reference.</param>
        /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
        /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsByAuthorUsernameAsync(string username, DateTimeOffset since, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["since"] = since.ToString("yyyy-MM-dd HH:mm:ss"),
                ["a"] = includeConvertedBeatmaps ? 1 : 0,
                ["type"] = "string",
                ["u"] = username
            };

            var request = await RequestAsync(Beatmaps, dict, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
        }

        /// <summary>
        ///     Gets lasts beatmaps published by the given author username since a specific <see cref="DateTimeOffset"/> on a specific <see cref="GameMode"/>.
        /// </summary>
        /// <param name="username">Username of the author.</param>
        /// <param name="since">Start <see cref="DateTimeOffset"> reference.</param>
        /// <param name="gameMode">Game mode of the beatmaps.</param>
        /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
        /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsByAuthorUsernameAsync(string username, DateTimeOffset since, GameMode gameMode = GameMode.Standard, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
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

            var request = await RequestAsync(Beatmaps, dict, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
        }

        /// <summary>
        ///     Gets lasts beatmaps published by the given author username.
        /// </summary>
        /// <param name="username">Username of the author.</param>
        /// <param name="includeConvertedBeatmaps">Indicates if we must include the converted beatmaps.</param>
        /// <param name="limit">Limit amount of beatmaps. Default and maximum to 500.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsByAuthorUsernameAsync(string username, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["a"] = includeConvertedBeatmaps ? 1 : 0,
                ["type"] = "string",
                ["u"] = username
            };

            var request = await RequestAsync(Beatmaps, dict, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
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
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsByAuthorUsernameAsync(string username, GameMode gameMode = GameMode.Standard, bool includeConvertedBeatmaps = true, int limit = 500, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>
            {
                ["limit"] = limit,
                ["a"] = includeConvertedBeatmaps ? 1 : 0,
                ["type"] = "string",
                ["u"] = username,
                ["m"] = (int)gameMode
            };

            var request = await RequestAsync(Beatmaps, dict, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
        }

        /// <summary>
        ///     Gets a set of beatmaps depending on the beatmapset id.
        /// </summary>
        /// <param name="beatmapsetId">Id of the beatmapset.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsetAsync(long beatmapsetId, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>
            {
                ["s"] = beatmapsetId
            };

            var request = await RequestAsync(Beatmaps, dict, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
        }

        /// <summary>
        ///      Gets a set of beatmaps on a specific <see cref="GameMode"/> depending on the beatmapset id.
        /// </summary>
        /// <param name="beatmapsetId">Id of the beatmapset.</param>
        /// <param name="gameMode">Game mode of the beatmaps.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<Beatmap>> GetBeatmapsetAsync(long beatmapsetId, GameMode gameMode = GameMode.Standard, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>
            {
                ["m"] = (int)gameMode,
                ["s"] = beatmapsetId
            };

            var request = await RequestAsync(Beatmaps, dict, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
        }

        /// <summary>
        ///     Gets a beatmap by it's id.
        /// </summary>
        /// <param name="beatmapId">Id of the beatmap.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns></returns>
        public async Task<Beatmap> GetBeatmapByIdAsync(long beatmapId, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>
            {
                ["b"] = beatmapId
            };

            var request = await RequestAsync(Beatmaps, dict, token).ConfigureAwait(false);

            var data = JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
            if (data.Count > 0)
            {
                return data[0];
            }

            return null;
        }

        /// <summary>
        ///     Gets a beatmap corresponding to the given replay hash.
        /// </summary>
        /// <param name="hash">Hash of a replay.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns></returns>
        public async Task<Beatmap> GetBeatmapByHashAsync(string hash, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>
            {
                ["h"] = hash
            };

            var request = await RequestAsync(Beatmaps, dict, token).ConfigureAwait(false);

            var data = JsonConvert.DeserializeObject<IReadOnlyList<Beatmap>>(request);
            if (data.Count > 0)
            {
                return data[0];
            }

            return null;
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
        public async Task<Replay> GetReplayByUserIdAsync(long beatmapId, long userId, GameMode gameMode = GameMode.Standard, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>
            {
                ["b"] = beatmapId,
                ["u"] = userId,
                ["m"] = (int)gameMode,
                ["type"] = "id"
            };

            var request = await RequestAsync(Replay, ReplayRateLimiter, dict, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Replay>(request);
        }

        /// <summary>
        ///     Gets a replay.
        /// </summary>
        /// <param name="beatmapId">Id of the beatmap.</param>
        /// <param name="username">Username of the player.</param>
        /// <param name="gameMode">Game mode the play has been played in.</param>
        /// <param name="token">Cancellation token used to cancel the current request.</param>
        /// <returns></returns>
        public async Task<Replay> GetReplayByUsernameAsync(long beatmapId, string username, GameMode gameMode = GameMode.Standard, CancellationToken token = default)
        {
            var dict = new Dictionary<string, object>
            {
                ["b"] = beatmapId,
                ["u"] = username,
                ["m"] = (int)gameMode,
                ["type"] = "string"
            };

            var request = await RequestAsync(Replay, ReplayRateLimiter, dict, token).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<Replay>(request);
        }

        #endregion

        private Task<string> RequestAsync(string endpoint, Dictionary<string, object> parameters = null, CancellationToken token = default)
        {
            return RequestAsync(endpoint, RateLimiter, parameters, token);
        }

        private async Task<string> RequestAsync(string endpoint, RateLimiter rateLimiter, Dictionary<string, object> parameters = null, CancellationToken token = default)
        {
            await rateLimiter.HandleAsync().ConfigureAwait(false);
            rateLimiter.IncrementRequestCount();

            var url = $"{Root}{endpoint}?k={OsuSharpConfiguration.ApiKey}";

            if (parameters != null && parameters.Count > 0)
            {
                var builder = new StringBuilder();
                foreach (var kvp in parameters)
                {
                    builder.Append($"&{kvp.Key}={kvp.Value}");
                }

                url += builder.ToString();
            }

            var response = await OsuSharpConfiguration.Client.GetAsync(url, token).ConfigureAwait(false);
            var message = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return message;
            }

            throw new OsuSharpException(message);
        }
    }
}
