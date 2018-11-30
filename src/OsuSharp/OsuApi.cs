using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OsuSharp.Endpoints;
using OsuSharp.Entities;
using OsuSharp.Enums;
using OsuSharp.Interfaces;
using OsuSharp.Misc;

namespace OsuSharp
{
    public sealed class OsuApi : IOsuApi
    {
        private const string ROOT_DOMAIN = "https://osu.ppy.sh";
        private const string GET_BEATMAPS_URL = "/api/get_beatmaps";
        private const string GET_USER_URL = "/api/get_user";
        private const string GET_SCORES_URL = "/api/get_scores";
        private const string GET_USER_BEST_URL = "/api/get_user_best";
        private const string GET_USER_RECENT_URL = "/api/get_user_recent";
        private const string GET_MATCH_URL = "/api/get_match";
        private const string GET_REPLAY_URL = "/api/get_replay";
        private const string API_KEY_PARAMETER = "?k=";
        private const string USER_PARAMETER = "&u=";
        private const string MATCH_PARAMETER = "&mp=";
        private const string LIMIT_PARAMETER = "&limit=";
        private const string BEATMAP_PARAMETER = "&b=";

        private static HttpClient _httpClient;

        /// <summary>
        ///     Internal custom API rate limiter.
        /// </summary>
        internal IRateLimiter Limiter { get; }

        /// <summary>
        ///     ApiKey from the osu! API.
        /// </summary>
        public string ApiKey { get; internal set; }

        /// <summary>
        ///     Separator used between each mod.
        /// </summary>
        public string ModsSeparator { get; set; }

        /// <summary>
        ///     OsuSharp logger.
        /// </summary>
        public IOsuSharpLogger Logger { get; }

        /// <summary>
        ///     Method that initializes the library to perform your requests.
        /// </summary>
        /// <param name="config">
        ///     OsuSharp configuration that contains api key, rate limiter settings, custom http client and mods
        ///     separator.
        /// </param>
        public OsuApi(IOsuSharpConfiguration config)
        {
            if (string.IsNullOrWhiteSpace(config.ApiKey))
            {
                throw new ArgumentException("The given api key was null or whitespace", nameof(config.ApiKey));
            }

            if (_httpClient == null && config.CustomHttpClient == null)
            {
                _httpClient = new HttpClient();
            }
            else if (config.CustomHttpClient != null)
            {
                _httpClient = config.CustomHttpClient;
            }

            ApiKey = config.ApiKey;
            ModsSeparator = config.ModsSeparator;

            Logger = new OsuSharpLogger(this, config.LogLevel);

            Limiter = new RateLimiter(config.MaxRequests, config.TimeInterval, config.ThrowOnMaxRequests, Logger);
        }

        /// <summary>
        ///     Method that initializes the library to perform your requests.
        /// </summary>
        /// <param name="config">
        ///     OsuSharp configuration that contains api key, rate limiter settings, custom http client and mods
        ///     separator.
        /// </param>
        public static OsuApi Default(IOsuSharpConfiguration config)
            => new OsuApi(config);

        /// <summary>
        ///     Method that returns a <see cref="Beatmap" />. It requires a valid BeatmapId.
        /// </summary>
        /// <param name="beatmapId">Id of the beatmap.</param>
        /// <param name="bmType">Type of the beatmap. Beatmapset or difficulty.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <returns>
        ///     <see cref="Beatmap" />
        /// </returns>
        public Beatmap GetBeatmap(long beatmapId, BeatmapType bmType = BeatmapType.ByDifficulty,
            GameMode gameMode = GameMode.Standard)
        {
            var mode = UserMode.ToString(gameMode);
            var type = BeatmapParam.ToString(bmType);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_beatmap called: {GetNameValues(beatmapId, bmType, gameMode)}", DateTime.Now);

            var request = Get($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{beatmapId}{mode}");

            var r = JsonConvert.DeserializeObject<List<Beatmap>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        ///     Method that returns a <see cref="Beatmap" />. It requires a valid BeatmapId.
        /// </summary>
        /// <param name="beatmapId">Id of the beatmap.</param>
        /// <param name="bmType">Type of the beatmap. Beatmapset or difficulty.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="Beatmap" />
        /// </returns>
        public async Task<Beatmap> GetBeatmapAsync(long beatmapId, BeatmapType bmType = BeatmapType.ByDifficulty,
            GameMode gameMode = GameMode.Standard, CancellationToken cancellationToken = default(CancellationToken))
        {
            var mode = UserMode.ToString(gameMode);
            var type = BeatmapParam.ToString(bmType);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_beatmap called: {GetNameValues(beatmapId, bmType, gameMode)}", DateTime.Now);

            var request =
                await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{beatmapId}{mode}",
                    cancellationToken).ConfigureAwait(false);

            var r = JsonConvert.DeserializeObject<List<Beatmap>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        ///     Method that returns a list of <see cref="Beatmap" /> by the given creator's nickname.
        /// </summary>
        /// <param name="username">Author's nickname of the beatmap.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public List<Beatmap> GetBeatmapsByCreator(string username, GameMode gameMode = GameMode.Standard,
            int limit = 500)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            var mode = UserMode.ToString(gameMode);
            var type = BeatmapParam.ToString(BeatmapType.ByCreator);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_beatmap called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            var request =
                Get($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{username}{LIMIT_PARAMETER}{limit}{mode}");

            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <summary>
        ///     Method that returns a list of <see cref="Beatmap" /> by the given creator's nickname.
        /// </summary>
        /// <param name="username">Author's nickname of the beatmap.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public async Task<List<Beatmap>> GetBeatmapsByCreatorAsync(string username, GameMode gameMode = GameMode.Standard,
            int limit = 500, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            var mode = UserMode.ToString(gameMode);
            var type = BeatmapParam.ToString(BeatmapType.ByCreator);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_beatmap called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            var request =
                await GetAsync(
                    $"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{username}{LIMIT_PARAMETER}{limit}{mode}",
                    cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <summary>
        ///     Method that returns a list of <see cref="Beatmap" /> by the given beatmapset id.
        /// </summary>
        /// <param name="id">Id of the beatmapset.</param>
        /// <param name="bmType">Type of the beatmap. ByBeatmap is required.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the ouput. Default and maximum : 500.</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public List<Beatmap> GetBeatmaps(long id, BeatmapType bmType = BeatmapType.ByBeatmap,
            GameMode gameMode = GameMode.Standard, int limit = 500)
        {
            var mode = UserMode.ToString(gameMode);
            var type = BeatmapParam.ToString(bmType);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_beatmap called: {GetNameValues(id, bmType, gameMode, limit)}", DateTime.Now);

            var request =
                Get($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{id}{LIMIT_PARAMETER}{limit}{mode}");

            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <summary>
        ///     Method that returns a list of <see cref="Beatmap" /> by the given beatmapset id.
        /// </summary>
        /// <param name="id">Id of the beatmapset.</param>
        /// <param name="bmType">Type of the beatmap. ByBeatmap is required.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the ouput. Default and maximum : 500.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public async Task<List<Beatmap>> GetBeatmapsAsync(long id, BeatmapType bmType = BeatmapType.ByBeatmap,
            GameMode gameMode = GameMode.Standard, int limit = 500, CancellationToken cancellationToken = default(CancellationToken))
        {
            var mode = UserMode.ToString(gameMode);
            var type = BeatmapParam.ToString(bmType);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_beatmap called: {GetNameValues(id, bmType, gameMode, limit)}", DateTime.Now);

            var request =
                await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{id}{LIMIT_PARAMETER}{limit}{mode}",
                    cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <summary>
        ///     Method that returns a list of lasts uploaded <see cref="Beatmap" />.
        /// </summary>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public List<Beatmap> GetLastBeatmaps(int limit = 500)
        {
            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", $"/api/get_beatmap called: {GetNameValues(limit)}",
                DateTime.Now);

            var request = Get($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{LIMIT_PARAMETER}{limit}");

            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <summary>
        ///     Method that returns a list of lasts uploaded <see cref="Beatmap" />.
        /// </summary>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public async Task<List<Beatmap>> GetLastBeatmapsAsync(int limit, CancellationToken cancellationToken = default(CancellationToken))
        {
            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", $"/api/get_beatmap called: {GetNameValues(limit)}",
                DateTime.Now);

            var request = await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{LIMIT_PARAMETER}{limit}",
                cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <summary>
        ///     Method that returns a <see cref="User" /> by the given Username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <returns>
        ///     <see cref="User" />
        /// </returns>
        public User GetUserByName(string username, GameMode gameMode = GameMode.Standard)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user called: {GetNameValues(username, gameMode)}", DateTime.Now);

            var request = Get($"{GET_USER_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}");

            var r = JsonConvert.DeserializeObject<List<User>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        ///     Method that returns a <see cref="User" /> by the given Username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="User" />
        /// </returns>
        public async Task<User> GetUserByNameAsync(string username, GameMode gameMode = GameMode.Standard,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user called: {GetNameValues(username, gameMode)}", DateTime.Now);

            var request = await GetAsync(
                $"{GET_USER_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}",
                cancellationToken).ConfigureAwait(false);

            var r = JsonConvert.DeserializeObject<List<User>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        ///     Method that returns a <see cref="User" /> by the given Userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <returns>
        ///     <see cref="User" />
        /// </returns>
        public User GetUserById(long userid, GameMode gameMode = GameMode.Standard)
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user called: {GetNameValues(userid, gameMode)}", DateTime.Now);

            var request = Get($"{GET_USER_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}");

            var r = JsonConvert.DeserializeObject<List<User>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        ///     Method that returns a <see cref="User" /> by the given Userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="User" />
        /// </returns>
        public async Task<User> GetUserByIdAsync(long userid, GameMode gameMode = GameMode.Standard,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user called: {GetNameValues(userid, gameMode)}", DateTime.Now);

            var request = await GetAsync($"{GET_USER_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}",
                cancellationToken).ConfigureAwait(false);

            var r = JsonConvert.DeserializeObject<List<User>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        ///     Method that returns a <see cref="Score" /> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns>
        ///     <see cref="Score" />
        /// </returns>
        public Score GetScoreByUsername(long beatmapid, string username, GameMode gameMode = GameMode.Standard)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, username, gameMode)}", DateTime.Now);

            var request =
                Get(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{USER_PARAMETER}{username}{BEATMAP_PARAMETER}{beatmapid}");

            var r = JsonConvert.DeserializeObject<List<Score>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        ///     Method that returns a <see cref="Score" /> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="Score" />
        /// </returns>
        public async Task<Score> GetScoreByUsernameAsync(long beatmapid, string username, GameMode gameMode = GameMode.Standard,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, username, gameMode)}", DateTime.Now);

            var request =
                await GetAsync(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{USER_PARAMETER}{username}{BEATMAP_PARAMETER}{beatmapid}",
                    cancellationToken).ConfigureAwait(false);

            var r = JsonConvert.DeserializeObject<List<Score>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        ///     Method that returns a <see cref="Score" /> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns>
        ///     <see cref="Score" />
        /// </returns>
        public Score GetScoreByUserid(long beatmapid, ulong userid, GameMode gameMode = GameMode.Standard)
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, userid, gameMode)}", DateTime.Now);

            var request =
                Get(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{USER_PARAMETER}{userid}{BEATMAP_PARAMETER}{beatmapid}");

            var r = JsonConvert.DeserializeObject<List<Score>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        ///     Method that returns a <see cref="Score" /> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="Score" />
        /// </returns>
        public async Task<Score> GetScoreByUseridAsync(long beatmapid, long userid, GameMode gameMode = GameMode.Standard,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, userid, gameMode)}", DateTime.Now);

            var request =
                await GetAsync(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{USER_PARAMETER}{userid}{BEATMAP_PARAMETER}{beatmapid}",
                    cancellationToken).ConfigureAwait(false);

            var r = JsonConvert.DeserializeObject<List<Score>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        ///     Method that returns a list of <see cref="Score" /> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="Score" />
        /// </returns>
        public List<Score> GetScores(long beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50)
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, gameMode, limit)}", DateTime.Now);

            var request =
                Get(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}");

            return JsonConvert.DeserializeObject<List<Score>>(request);
        }

        /// <summary>
        ///     Method that returns a list of <see cref="Score" /> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="Score" />
        /// </returns>
        public async Task<List<Score>> GetScoresAsync(long beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, gameMode, limit)}", DateTime.Now);

            var request =
                await GetAsync(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}",
                    cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<Score>>(request);
        }

        /// <summary>
        ///     Method that returns a list of <see cref="BeatmapScores" /> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="BeatmapScores" />
        /// </returns>
        public BeatmapScores GetScoresAndBeatmap(long beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50)
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, gameMode)}", DateTime.Now);

            var request =
                Get(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}");

            var beatmap = GetBeatmap(beatmapid, gameMode: gameMode);

            var score = JsonConvert.DeserializeObject<List<Score>>(request);
            return new BeatmapScores
            {
                Beatmap = beatmap,
                Score = score
            };
        }

        /// <summary>
        ///     Method that returns a list of <see cref="BeatmapScores" /> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="BeatmapScores" />
        /// </returns>
        public async Task<BeatmapScores> GetScoresAndBeatmapAsync(long beatmapid, GameMode gameMode = GameMode.Standard,
            int limit = 50, CancellationToken cancellationToken = default(CancellationToken))
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, gameMode, limit)}", DateTime.Now);

            var request =
                await GetAsync(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}",
                    cancellationToken).ConfigureAwait(false);

            var beatmap = await GetBeatmapAsync(beatmapid, BeatmapType.ByDifficulty, gameMode, cancellationToken)
                .ConfigureAwait(false);

            var score = JsonConvert.DeserializeObject<List<Score>>(request);
            return new BeatmapScores
            {
                Beatmap = beatmap,
                Score = score
            };
        }

        /// <summary>
        ///     Method that returns a list of <see cref="BeatmapScoresUsers" /> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="BeatmapScoresUsers" />
        /// </returns>
        public BeatmapScoresUsers GetScoresWithUsersAndBeatmap(long beatmapid, GameMode gameMode = GameMode.Standard,
            int limit = 50)
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, gameMode, limit)}", DateTime.Now);

            var request =
                Get(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}");

            var beatmap = GetBeatmap(beatmapid);

            var users = new List<User>();
            var scores = JsonConvert.DeserializeObject<List<Score>>(request);
            foreach (var score in scores)
            {
                users.Add(GetUserById(score.Userid, gameMode));
            }

            return new BeatmapScoresUsers
            {
                Beatmap = beatmap,
                Scores = scores,
                Users = users
            };
        }

        /// <summary>
        ///     Method that returns a list of <see cref="BeatmapScoresUsers" /> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="BeatmapScoresUsers" />
        /// </returns>
        public async Task<BeatmapScoresUsers> GetScoresWithUsersAndBeatmapAsync(long beatmapid, GameMode gameMode = GameMode.Standard,
            int limit = 50, CancellationToken cancellationToken = default(CancellationToken))
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, gameMode, limit)}", DateTime.Now);

            var request =
                await GetAsync(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}",
                    cancellationToken).ConfigureAwait(false);

            var beatmap = await GetBeatmapAsync(beatmapid, cancellationToken: cancellationToken).ConfigureAwait(false);

            var users = new List<User>();
            var scores = JsonConvert.DeserializeObject<List<Score>>(request);
            foreach (var score in scores)
            {
                users.Add(await GetUserByIdAsync(score.Userid, gameMode, cancellationToken).ConfigureAwait(false));
            }

            return new BeatmapScoresUsers
            {
                Beatmap = beatmap,
                Scores = scores,
                Users = users
            };
        }

        /// <summary>
        ///     Method that returns a list of <see cref="UserBest" /> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public List<UserBest> GetUserBestByUsername(string username, GameMode gameMode = GameMode.Standard,
            int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_best called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            var request =
                Get(
                    $"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}");

            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        /// <summary>
        ///     Method that returns a list of <see cref="UserBest" /> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public async Task<List<UserBest>> GetUserBestByUsernameAsync(string username, GameMode gameMode = GameMode.Standard,
            int limit = 10, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_best called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            var request =
                await GetAsync(
                    $"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}",
                    cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        /// <summary>
        ///     Method that returns a list of <see cref="UserBestBeatmap" /> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public List<UserBestBeatmap> GetUserBestAndBeatmapByUsername(string username,
            GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_best called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            var request =
                Get(
                    $"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}");

            var userBestBeatmap = new List<UserBestBeatmap>();
            var userBest = JsonConvert.DeserializeObject<List<UserBest>>(request);
            foreach (var best in userBest)
            {
                userBestBeatmap.Add(new UserBestBeatmap
                {
                    Beatmap = GetBeatmap(best.BeatmapId),
                    UserBest = best
                });
            }

            return userBestBeatmap;
        }

        /// <summary>
        ///     Method that returns a list of <see cref="UserBestBeatmap" /> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public async Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUsernameAsync(string username,
            GameMode gameMode = GameMode.Standard, int limit = 10, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_best called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            var request =
                await GetAsync(
                    $"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}",
                    cancellationToken).ConfigureAwait(false);

            var userBestBeatmap = new List<UserBestBeatmap>();
            var userBest = JsonConvert.DeserializeObject<List<UserBest>>(request);
            foreach (var best in userBest)
            {
                userBestBeatmap.Add(new UserBestBeatmap
                {
                    Beatmap = await GetBeatmapAsync(best.BeatmapId, BeatmapType.ByDifficulty, gameMode,
                        cancellationToken).ConfigureAwait(false),
                    UserBest = best
                });
            }

            return userBestBeatmap;
        }

        /// <summary>
        ///     Method that returns a list of <see cref="UserBest" /> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public List<UserBest> GetUserBestByUserid(long userid, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_best called: {GetNameValues(userid, gameMode, limit)}", DateTime.Now);

            var request =
                Get(
                    $"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}");

            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        /// <summary>
        ///     Method that returns a list of <see cref="UserBest" /> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public async Task<List<UserBest>> GetUserBestByUseridAsync(long userid, GameMode gameMode = GameMode.Standard,
            int limit = 10, CancellationToken cancellationToken = default(CancellationToken))
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_best called: {GetNameValues(userid, gameMode, limit)}", DateTime.Now);

            var request =
                await GetAsync(
                    $"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}",
                    cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        /// <summary>
        ///     Method that returns a list of <see cref="UserBestBeatmap" /> by the given username.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public List<UserBestBeatmap> GetUserBestAndBeatmapByUserid(long userid, GameMode gameMode = GameMode.Standard,
            int limit = 10)
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_best called: {GetNameValues(userid, gameMode, limit)}", DateTime.Now);

            var request =
                Get(
                    $"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}");

            var userBestBeatmap = new List<UserBestBeatmap>();
            var userBest = JsonConvert.DeserializeObject<List<UserBest>>(request);
            foreach (var best in userBest)
            {
                userBestBeatmap.Add(new UserBestBeatmap
                {
                    Beatmap = GetBeatmap(best.BeatmapId),
                    UserBest = best
                });
            }

            return userBestBeatmap;
        }

        /// <summary>
        ///     Method that returns a list of <see cref="UserBestBeatmap" /> by the given username.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public async Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUseridAsync(long userid, GameMode gameMode = GameMode.Standard,
            int limit = 10, CancellationToken cancellationToken = default(CancellationToken))
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_best called: {GetNameValues(userid, gameMode, limit)}", DateTime.Now);

            var request =
                await GetAsync(
                    $"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}",
                    cancellationToken).ConfigureAwait(false);

            var userBestBeatmap = new List<UserBestBeatmap>();
            var userBest = JsonConvert.DeserializeObject<List<UserBest>>(request);
            foreach (var best in userBest)
            {
                userBestBeatmap.Add(new UserBestBeatmap
                {
                    Beatmap = await GetBeatmapAsync(best.BeatmapId, BeatmapType.ByDifficulty, gameMode,
                        cancellationToken).ConfigureAwait(false),
                    UserBest = best
                });
            }

            return userBestBeatmap;
        }

        /// <summary>
        ///     Method that returns a list of <see cref="UserRecent" /> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public List<UserRecent> GetUserRecentByUsername(string username, GameMode gameMode = GameMode.Standard,
            int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_recent called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            var request =
                Get(
                    $"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}");

            return JsonConvert.DeserializeObject<List<UserRecent>>(request);
        }

        /// <summary>
        ///     Method that returns a list of <see cref="UserRecent" /> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public async Task<List<UserRecent>> GetUserRecentByUsernameAsync(string username, GameMode gameMode = GameMode.Standard,
            int limit = 10, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_recent called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            var request =
                await GetAsync(
                    $"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}",
                    cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<UserRecent>>(request);
        }

        /// <summary>
        ///     Method that returns a list of <see cref="UserRecentBeatmap" /> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public List<UserRecentBeatmap> GetUserRecentAndBeatmapByUsername(string username,
            GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_recent called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            var request =
                Get(
                    $"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}");

            var userRecentBeatmap = new List<UserRecentBeatmap>();
            var userRecents = JsonConvert.DeserializeObject<List<UserRecent>>(request);
            foreach (var recent in userRecents)
            {
                userRecentBeatmap.Add(new UserRecentBeatmap
                {
                    Beatmap = GetBeatmap(recent.BeatmapId),
                    UserRecent = recent
                });
            }

            return userRecentBeatmap;
        }

        /// <summary>
        ///     Method that returns a list of <see cref="UserRecentBeatmap" /> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public async Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUsernameAsync(string username,
            GameMode gameMode = GameMode.Standard, int limit = 10, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_recent called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            var request =
                await GetAsync(
                    $"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}",
                    cancellationToken).ConfigureAwait(false);

            var userRecentBeatmap = new List<UserRecentBeatmap>();
            var userRecents = JsonConvert.DeserializeObject<List<UserRecent>>(request);
            foreach (var recent in userRecents)
            {
                userRecentBeatmap.Add(new UserRecentBeatmap
                {
                    Beatmap = await GetBeatmapAsync(recent.BeatmapId, BeatmapType.ByDifficulty, gameMode,
                        cancellationToken).ConfigureAwait(false),
                    UserRecent = recent
                });
            }

            return userRecentBeatmap;
        }

        /// <summary>
        ///     Method that returns a list of <see cref="UserRecent" /> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public List<UserRecent> GetUserRecentByUserid(long userid, GameMode gameMode = GameMode.Standard,
            int limit = 10)
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_recent called: {GetNameValues(userid, gameMode, limit)}", DateTime.Now);

            var request =
                Get(
                    $"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}");

            return JsonConvert.DeserializeObject<List<UserRecent>>(request);
        }

        /// <summary>
        ///     Method that returns a list of <see cref="UserRecent" /> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public async Task<List<UserRecent>> GetUserRecentByUseridAsync(long userid, GameMode gameMode = GameMode.Standard,
            int limit = 10, CancellationToken cancellationToken = default(CancellationToken))
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_recent called: {GetNameValues(userid, gameMode, limit)}", DateTime.Now);

            var request =
                await GetAsync(
                    $"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}",
                    cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<UserRecent>>(request);
        }

        /// <summary>
        ///     Method that returns a list of <see cref="UserRecentBeatmap" /> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public List<UserRecentBeatmap> GetUserRecentAndBeatmapByUserid(long userid,
            GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_recent called: {GetNameValues(userid, gameMode, limit)}", DateTime.Now);

            var request =
                Get(
                    $"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}");

            var userRecentBeatmap = new List<UserRecentBeatmap>();
            var userRecents = JsonConvert.DeserializeObject<List<UserRecent>>(request);
            foreach (var recent in userRecents)
            {
                userRecentBeatmap.Add(new UserRecentBeatmap
                {
                    Beatmap = GetBeatmap(recent.BeatmapId),
                    UserRecent = recent
                });
            }

            return userRecentBeatmap;
        }

        /// <summary>
        ///     Method that returns a list of <see cref="UserRecentBeatmap" /> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="List{T}" />
        /// </returns>
        public async Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUseridAsync(long userid, GameMode gameMode = GameMode.Standard,
            int limit = 10, CancellationToken cancellationToken = default(CancellationToken))
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", "/api/get_user_recent called: ", DateTime.Now);

            var request =
                await GetAsync(
                    $"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}",
                    cancellationToken).ConfigureAwait(false);

            var userRecentBeatmap = new List<UserRecentBeatmap>();
            var userRecents = JsonConvert.DeserializeObject<List<UserRecent>>(request);
            foreach (var recent in userRecents)
            {
                userRecentBeatmap.Add(new UserRecentBeatmap
                {
                    Beatmap = await GetBeatmapAsync(recent.BeatmapId, BeatmapType.ByDifficulty, gameMode,
                        cancellationToken).ConfigureAwait(false),
                    UserRecent = recent
                });
            }

            return userRecentBeatmap;
        }

        /// <summary>
        ///     Method that returns a <see cref="Multiplayer" /> by the given matchid.
        /// </summary>
        /// <param name="matchid">Id of the match.</param>
        /// <returns>
        ///     <see cref="Multiplayer" />
        /// </returns>
        public Multiplayer GetMatch(long matchid)
        {
            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", "/api/get_match called: ", DateTime.Now);

            var request = Get($"{GET_MATCH_URL}{API_KEY_PARAMETER}{ApiKey}{MATCH_PARAMETER}{matchid}");

            var r = JsonConvert.DeserializeObject<List<Multiplayer>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        ///     Method that returns a <see cref="Multiplayer" /> by the given matchid.
        /// </summary>
        /// <param name="matchid">Id of the match.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="Multiplayer" />
        /// </returns>
        public async Task<Multiplayer> GetMatchAsync(long matchid, CancellationToken cancellationToken = default(CancellationToken))
        {
            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", "/api/get_match called:", DateTime.Now);

            var request =
                await GetAsync($"{GET_MATCH_URL}{API_KEY_PARAMETER}{ApiKey}{MATCH_PARAMETER}{matchid}",
                    cancellationToken).ConfigureAwait(false);

            var r = JsonConvert.DeserializeObject<List<Multiplayer>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        ///     Method that returns a <see cref="Replay" /> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns>
        ///     <see cref="Replay" />
        /// </returns>
        public Replay GetReplayByUsername(long beatmapid, string username, GameMode gameMode = GameMode.Standard)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", "/api/get_replay called: ", DateTime.Now);

            var request =
                Get(
                    $"{GET_REPLAY_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{BEATMAP_PARAMETER}{beatmapid}{USER_PARAMETER}{username}");

            return JsonConvert.DeserializeObject<Replay>(request);
        }

        /// <summary>
        ///     Method that returns a <see cref="Replay" /> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="Replay" />
        /// </returns>
        public async Task<Replay> GetReplayByUsernameAsync(long beatmapid, string username, GameMode gameMode = GameMode.Standard,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", "/api/get_replay called: ", DateTime.Now);

            var request =
                await GetAsync(
                    $"{GET_REPLAY_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{BEATMAP_PARAMETER}{beatmapid}{USER_PARAMETER}{username}",
                    cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Replay>(request);
        }

        /// <summary>
        ///     Method that returns a <see cref="Replay" /> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns>
        ///     <see cref="Replay" />
        /// </returns>
        public Replay GetReplayByUserid(long beatmapid, long userid, GameMode gameMode = GameMode.Standard)
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", "/api/get_replay called: ", DateTime.Now);

            var request =
                Get(
                    $"{GET_REPLAY_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{BEATMAP_PARAMETER}{beatmapid}{USER_PARAMETER}{userid}");

            return JsonConvert.DeserializeObject<Replay>(request);
        }

        /// <summary>
        ///     Method that returns a <see cref="Replay" /> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>
        ///     <see cref="Replay" />
        /// </returns>
        public async Task<Replay> GetReplayByUseridAsync(long beatmapid, long userid, GameMode gameMode = GameMode.Standard,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", "/api/get_replay called: ", DateTime.Now);

            var request =
                await GetAsync(
                    $"{GET_REPLAY_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{BEATMAP_PARAMETER}{beatmapid}{USER_PARAMETER}{userid}",
                    cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<Replay>(request);
        }

        /// <summary>
        ///     Create a replay from api entities.
        ///     You shouldn't mix up replays, users, beatmaps and scores, but.. you could.
        /// </summary>
        /// <param name="filename">Name of the file to create</param>
        /// <param name="replay">Replay entity</param>
        /// <param name="user">User entity</param>
        /// <param name="score">Score entity</param>
        /// <param name="beatmap">Beatmap entity</param>
        /// <returns></returns>
        public void CreateReplayFile(string filename, Replay replay, User user, Score score, Beatmap beatmap)
        {
            filename = filename.EndsWith(".osr") ? filename : filename + ".osr";

            var rp = ReplayFile.CreateReplayFile(replay, user, score, beatmap);
            var fs = new FileStream(filename, FileMode.OpenOrCreate);

            rp.ToStream(fs);
            fs.Close();
        }

        private string Get(string url)
        {
            Limiter.HandleAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            var message = _httpClient.GetAsync(ROOT_DOMAIN + url)
                .ConfigureAwait(false).GetAwaiter().GetResult();

            if (message.StatusCode == HttpStatusCode.OK)
            {
                return message.Content.ReadAsStringAsync()
                    .ConfigureAwait(false).GetAwaiter().GetResult();
            }

            throw new OsuSharpException(message.Content.ReadAsStringAsync()
                .ConfigureAwait(false).GetAwaiter().GetResult());
        }

        private async Task<string> GetAsync(string url, CancellationToken cancellationToken = default(CancellationToken))
        {
            await Limiter.HandleAsync(cancellationToken).ConfigureAwait(false);

            var message =
                await _httpClient.GetAsync(ROOT_DOMAIN + url, cancellationToken).ConfigureAwait(false);

            if (message.StatusCode == HttpStatusCode.OK)
            {
                return await message.Content.ReadAsStringAsync().ConfigureAwait(false);
            }

            throw new OsuSharpException(await message.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        private string GetNameValues(params object[] parameters)
        {
            var builder = new StringBuilder();

            foreach (var param in parameters)
            {
                builder.Append($"[{param.GetType().Name}: {param}] ");
            }

            return builder.ToString();
        }
    }
}