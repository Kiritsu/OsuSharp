using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OsuSharp.BeatmapEndpoint;
using OsuSharp.Entities;
using OsuSharp.Interfaces;
using OsuSharp.MatchEndpoint;
using OsuSharp.Misc;
using OsuSharp.ReplayEndpoint;
using OsuSharp.ScoreEndpoint;
using OsuSharp.UserBestEndpoint;
using OsuSharp.UserEndpoint;
using OsuSharp.UserRecentEndpoint;

namespace OsuSharp
{
    public class OsuApi : IOsuApi
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
        ///     Internal api calls rate limiter.
        /// </summary>
        internal IRateLimiter Limiter { get; }

        /// <inheritdoc />
        public string ApiKey { get; internal set; }

        /// <inheritdoc />
        public string ModsSeparator { get; set; }

        /// <inheritdoc />
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
                throw new ArgumentException("The given api key was null or whitespace", nameof(config.ApiKey));

            if (_httpClient == null && config.CustomHttpClient == null)
                _httpClient = new HttpClient();
            else if (config.CustomHttpClient != null) _httpClient = config.CustomHttpClient;

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
        {
            return new OsuApi(config);
        }

        /// <inheritdoc />
        public Beatmap GetBeatmap(ulong beatmapId, BeatmapType bmType = BeatmapType.ByDifficulty,
            GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string type = BeatmapParam.ToString(bmType);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_beatmap called: {GetNameValues(beatmapId, bmType, gameMode)}", DateTime.Now);

            string request = Get($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{beatmapId}{mode}");

            List<Beatmap> r = JsonConvert.DeserializeObject<List<Beatmap>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public async Task<Beatmap> GetBeatmapAsync(ulong beatmapId, BeatmapType bmType = BeatmapType.ByDifficulty,
            GameMode gameMode = GameMode.Standard)
        {
            return await GetBeatmapAsync(beatmapId, bmType, gameMode, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<Beatmap> GetBeatmapAsync(ulong beatmapId, BeatmapType bmType, GameMode gameMode,
            CancellationToken cancellationToken)
        {
            string mode = UserMode.ToString(gameMode);
            string type = BeatmapParam.ToString(bmType);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_beatmap called: {GetNameValues(beatmapId, bmType, gameMode)}", DateTime.Now);

            string request =
                await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{beatmapId}{mode}",
                    cancellationToken).ConfigureAwait(false);

            List<Beatmap> r = JsonConvert.DeserializeObject<List<Beatmap>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public List<Beatmap> GetBeatmapsByCreator(string username, GameMode gameMode = GameMode.Standard,
            int limit = 500)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("The given username was null or white space.", nameof(username));

            string mode = UserMode.ToString(gameMode);
            string type = BeatmapParam.ToString(BeatmapType.ByCreator);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_beatmap called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            string request =
                Get($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{username}{LIMIT_PARAMETER}{limit}{mode}");

            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <inheritdoc />
        public async Task<List<Beatmap>> GetBeatmapsByCreatorAsync(string username,
            GameMode gameMode = GameMode.Standard, int limit = 500)
        {
            return await GetBeatmapsByCreatorAsync(username, gameMode, limit, CancellationToken.None)
                .ConfigureAwait(false);
        }

        public async Task<List<Beatmap>> GetBeatmapsByCreatorAsync(string username, GameMode gameMode, int limit,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("The given username was null or white space.", nameof(username));

            string mode = UserMode.ToString(gameMode);
            string type = BeatmapParam.ToString(BeatmapType.ByCreator);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_beatmap called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            string request =
                await GetAsync(
                    $"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{username}{LIMIT_PARAMETER}{limit}{mode}",
                    cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <inheritdoc />
        public List<Beatmap> GetBeatmaps(ulong id, BeatmapType bmType = BeatmapType.ByBeatmap,
            GameMode gameMode = GameMode.Standard, int limit = 500)
        {
            string mode = UserMode.ToString(gameMode);
            string type = BeatmapParam.ToString(bmType);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_beatmap called: {GetNameValues(id, bmType, gameMode, limit)}", DateTime.Now);

            string request = Get($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{id}{LIMIT_PARAMETER}{limit}{mode}");

            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <inheritdoc />
        public async Task<List<Beatmap>> GetBeatmapsAsync(ulong id, BeatmapType bmType = BeatmapType.ByBeatmap,
            GameMode gameMode = GameMode.Standard, int limit = 500)
        {
            return await GetBeatmapsAsync(id, bmType, gameMode, limit, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<List<Beatmap>> GetBeatmapsAsync(ulong id, BeatmapType bmType, GameMode gameMode, int limit,
            CancellationToken cancellationToken)
        {
            string mode = UserMode.ToString(gameMode);
            string type = BeatmapParam.ToString(bmType);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_beatmap called: {GetNameValues(id, bmType, gameMode, limit)}", DateTime.Now);

            string request =
                await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{id}{LIMIT_PARAMETER}{limit}{mode}",
                    cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <inheritdoc />
        public List<Beatmap> GetLastBeatmaps(int limit = 500)
        {
            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", $"/api/get_beatmap called: {GetNameValues(limit)}",
                DateTime.Now);

            string request = Get($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{LIMIT_PARAMETER}{limit}");

            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <inheritdoc />
        public async Task<List<Beatmap>> GetLastBeatmapsAsync(int limit = 500)
        {
            return await GetLastBeatmapsAsync(limit, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<List<Beatmap>> GetLastBeatmapsAsync(int limit, CancellationToken cancellationToken)
        {
            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", $"/api/get_beatmap called: {GetNameValues(limit)}",
                DateTime.Now);

            string request = await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{LIMIT_PARAMETER}{limit}",
                cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <inheritdoc />
        public User GetUserByName(string username, GameMode gameMode = GameMode.Standard)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("The given username was null or white space.", nameof(username));

            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user called: {GetNameValues(username, gameMode)}", DateTime.Now);

            string request = Get($"{GET_USER_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}");

            List<User> r = JsonConvert.DeserializeObject<List<User>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public async Task<User> GetUserByNameAsync(string username, GameMode gameMode = GameMode.Standard)
        {
            return await GetUserByNameAsync(username, gameMode, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<User> GetUserByNameAsync(string username, GameMode gameMode,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("The given username was null or white space.", nameof(username));

            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user called: {GetNameValues(username, gameMode)}", DateTime.Now);

            string request = await GetAsync($"{GET_USER_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}",
                cancellationToken).ConfigureAwait(false);

            List<User> r = JsonConvert.DeserializeObject<List<User>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public User GetUserById(ulong userid, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user called: {GetNameValues(userid, gameMode)}", DateTime.Now);

            string request = Get($"{GET_USER_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}");

            List<User> r = JsonConvert.DeserializeObject<List<User>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public async Task<User> GetUserByIdAsync(ulong userid, GameMode gameMode = GameMode.Standard)
        {
            return await GetUserByIdAsync(userid, gameMode, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<User> GetUserByIdAsync(ulong userid, GameMode gameMode, CancellationToken cancellationToken)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user called: {GetNameValues(userid, gameMode)}", DateTime.Now);

            string request = await GetAsync($"{GET_USER_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}",
                cancellationToken).ConfigureAwait(false);

            List<User> r = JsonConvert.DeserializeObject<List<User>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public Score GetScoreByUsername(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("The given username was null or white space.", nameof(username));

            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, username, gameMode)}", DateTime.Now);

            string request =
                Get(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{USER_PARAMETER}{username}{BEATMAP_PARAMETER}{beatmapid}");

            List<Score> r = JsonConvert.DeserializeObject<List<Score>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public async Task<Score> GetScoreByUsernameAsync(ulong beatmapid, string username,
            GameMode gameMode = GameMode.Standard)
        {
            return await GetScoreByUsernameAsync(beatmapid, username, gameMode, CancellationToken.None)
                .ConfigureAwait(false);
        }

        public async Task<Score> GetScoreByUsernameAsync(ulong beatmapid, string username, GameMode gameMode,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("The given username was null or white space.", nameof(username));

            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, username, gameMode)}", DateTime.Now);

            string request =
                await GetAsync(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{USER_PARAMETER}{username}{BEATMAP_PARAMETER}{beatmapid}",
                    cancellationToken).ConfigureAwait(false);

            List<Score> r = JsonConvert.DeserializeObject<List<Score>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public Score GetScoreByUserid(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, userid, gameMode)}", DateTime.Now);

            string request =
                Get(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{USER_PARAMETER}{userid}{BEATMAP_PARAMETER}{beatmapid}");

            List<Score> r = JsonConvert.DeserializeObject<List<Score>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public async Task<Score> GetScoreByUseridAsync(ulong beatmapid, ulong userid,
            GameMode gameMode = GameMode.Standard)
        {
            return await GetScoreByUseridAsync(beatmapid, userid, gameMode, CancellationToken.None)
                .ConfigureAwait(false);
        }

        public async Task<Score> GetScoreByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode,
            CancellationToken cancellationToken)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, userid, gameMode)}", DateTime.Now);

            string request =
                await GetAsync(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{USER_PARAMETER}{userid}{BEATMAP_PARAMETER}{beatmapid}",
                    cancellationToken).ConfigureAwait(false);

            List<Score> r = JsonConvert.DeserializeObject<List<Score>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public List<Score> GetScores(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, gameMode, limit)}", DateTime.Now);

            string request =
                Get(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}");

            return JsonConvert.DeserializeObject<List<Score>>(request);
        }

        /// <inheritdoc />
        public async Task<List<Score>> GetScoresAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard,
            int limit = 50)
        {
            return await GetScoresAsync(beatmapid, gameMode, limit, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<List<Score>> GetScoresAsync(ulong beatmapid, GameMode gameMode, int limit,
            CancellationToken cancellationToken)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, gameMode, limit)}", DateTime.Now);

            string request =
                await GetAsync(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}",
                    cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<Score>>(request);
        }

        /// <inheritdoc />
        public BeatmapScores GetScoresAndBeatmap(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, gameMode)}", DateTime.Now);

            string request =
                Get(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}");

            Beatmap beatmap = GetBeatmap(beatmapid, gameMode: gameMode);

            List<Score> score = JsonConvert.DeserializeObject<List<Score>>(request);
            return new BeatmapScores
            {
                Beatmap = beatmap,
                Score = score
            };
        }

        /// <inheritdoc />
        public async Task<BeatmapScores> GetScoresAndBeatmapAsync(ulong beatmapid,
            GameMode gameMode = GameMode.Standard, int limit = 50)
        {
            return await GetScoresAndBeatmapAsync(beatmapid, gameMode, limit, CancellationToken.None)
                .ConfigureAwait(false);
        }

        public async Task<BeatmapScores> GetScoresAndBeatmapAsync(ulong beatmapid, GameMode gameMode, int limit,
            CancellationToken cancellationToken)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, gameMode, limit)}", DateTime.Now);

            string request =
                await GetAsync(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}",
                    cancellationToken).ConfigureAwait(false);

            Beatmap beatmap = await GetBeatmapAsync(beatmapid, BeatmapType.ByDifficulty, gameMode, cancellationToken)
                .ConfigureAwait(false);

            List<Score> score = JsonConvert.DeserializeObject<List<Score>>(request);
            return new BeatmapScores
            {
                Beatmap = beatmap,
                Score = score
            };
        }

        /// <inheritdoc />
        public BeatmapScoresUsers GetScoresWithUsersAndBeatmap(ulong beatmapid, GameMode gameMode = GameMode.Standard,
            int limit = 50)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, gameMode, limit)}", DateTime.Now);

            string request =
                Get(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}");

            Beatmap beatmap = GetBeatmap(beatmapid);

            List<User> users = new List<User>();
            List<Score> scores = JsonConvert.DeserializeObject<List<Score>>(request);
            foreach (Score score in scores) users.Add(GetUserById(score.Userid, gameMode));

            return new BeatmapScoresUsers
            {
                Beatmap = beatmap,
                Scores = scores,
                Users = users
            };
        }

        /// <inheritdoc />
        public async Task<BeatmapScoresUsers> GetScoresWithUsersAndBeatmapAsync(ulong beatmapid,
            GameMode gameMode = GameMode.Standard, int limit = 50)
        {
            return await GetScoresWithUsersAndBeatmapAsync(beatmapid, gameMode, limit, CancellationToken.None)
                .ConfigureAwait(false);
        }

        public async Task<BeatmapScoresUsers> GetScoresWithUsersAndBeatmapAsync(ulong beatmapid, GameMode gameMode,
            int limit, CancellationToken cancellationToken)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_score called: {GetNameValues(beatmapid, gameMode, limit)}", DateTime.Now);

            string request =
                await GetAsync(
                    $"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}",
                    cancellationToken).ConfigureAwait(false);

            Beatmap beatmap = await GetBeatmapAsync(beatmapid).ConfigureAwait(false);

            List<User> users = new List<User>();
            List<Score> scores = JsonConvert.DeserializeObject<List<Score>>(request);
            foreach (Score score in scores)
                users.Add(await GetUserByIdAsync(score.Userid, gameMode, cancellationToken).ConfigureAwait(false));

            return new BeatmapScoresUsers
            {
                Beatmap = beatmap,
                Scores = scores,
                Users = users
            };
        }

        /// <inheritdoc />
        public List<UserBest> GetUserBestByUsername(string username, GameMode gameMode = GameMode.Standard,
            int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("The given username was null or white space.", nameof(username));

            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_best called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            string request =
                Get(
                    $"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}");

            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        /// <inheritdoc />
        public async Task<List<UserBest>> GetUserBestByUsernameAsync(string username,
            GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            return await GetUserBestByUsernameAsync(username, gameMode, limit, CancellationToken.None);
        }

        public async Task<List<UserBest>> GetUserBestByUsernameAsync(string username, GameMode gameMode, int limit,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("The given username was null or white space.", nameof(username));

            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_best called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            string request =
                await GetAsync(
                    $"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}",
                    cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        /// <inheritdoc />
        public List<UserBestBeatmap> GetUserBestAndBeatmapByUsername(string username,
            GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("The given username was null or white space.", nameof(username));

            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_best called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            string request =
                Get(
                    $"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}");

            List<UserBestBeatmap> userBestBeatmap = new List<UserBestBeatmap>();
            List<UserBest> userBest = JsonConvert.DeserializeObject<List<UserBest>>(request);
            foreach (UserBest best in userBest)
                userBestBeatmap.Add(new UserBestBeatmap
                {
                    Beatmap = GetBeatmap(best.BeatmapId),
                    UserBest = best
                });

            return userBestBeatmap;
        }

        /// <inheritdoc />
        public async Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUsernameAsync(string username,
            GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            return await GetUserBestAndBeatmapByUsernameAsync(username, gameMode, limit, CancellationToken.None)
                .ConfigureAwait(false);
        }

        public async Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUsernameAsync(string username,
            GameMode gameMode, int limit, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("The given username was null or white space.", nameof(username));

            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_best called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            string request =
                await GetAsync(
                    $"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}",
                    cancellationToken).ConfigureAwait(false);

            List<UserBestBeatmap> userBestBeatmap = new List<UserBestBeatmap>();
            List<UserBest> userBest = JsonConvert.DeserializeObject<List<UserBest>>(request);
            foreach (UserBest best in userBest)
                userBestBeatmap.Add(new UserBestBeatmap
                {
                    Beatmap = await GetBeatmapAsync(best.BeatmapId, BeatmapType.ByDifficulty, gameMode,
                        cancellationToken).ConfigureAwait(false),
                    UserBest = best
                });

            return userBestBeatmap;
        }

        /// <inheritdoc />
        public List<UserBest> GetUserBestByUserid(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_best called: {GetNameValues(userid, gameMode, limit)}", DateTime.Now);

            string request =
                Get(
                    $"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}");

            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        /// <inheritdoc />
        public async Task<List<UserBest>> GetUserBestByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard,
            int limit = 10)
        {
            return await GetUserBestByUseridAsync(userid, gameMode, limit, CancellationToken.None)
                .ConfigureAwait(false);
        }

        public async Task<List<UserBest>> GetUserBestByUseridAsync(ulong userid, GameMode gameMode, int limit,
            CancellationToken cancellationToken)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_best called: {GetNameValues(userid, gameMode, limit)}", DateTime.Now);

            string request =
                await GetAsync(
                    $"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}",
                    cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        /// <inheritdoc />
        public List<UserBestBeatmap> GetUserBestAndBeatmapByUserid(ulong userid, GameMode gameMode = GameMode.Standard,
            int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_best called: {GetNameValues(userid, gameMode, limit)}", DateTime.Now);

            string request =
                Get(
                    $"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}");

            List<UserBestBeatmap> userBestBeatmap = new List<UserBestBeatmap>();
            List<UserBest> userBest = JsonConvert.DeserializeObject<List<UserBest>>(request);
            foreach (UserBest best in userBest)
                userBestBeatmap.Add(new UserBestBeatmap
                {
                    Beatmap = GetBeatmap(best.BeatmapId),
                    UserBest = best
                });

            return userBestBeatmap;
        }

        /// <inheritdoc />
        public async Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUseridAsync(ulong userid,
            GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            return await GetUserBestAndBeatmapByUseridAsync(userid, gameMode, limit, CancellationToken.None)
                .ConfigureAwait(false);
        }

        public async Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUseridAsync(ulong userid, GameMode gameMode,
            int limit, CancellationToken cancellationToken)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_best called: {GetNameValues(userid, gameMode, limit)}", DateTime.Now);

            string request =
                await GetAsync(
                    $"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}",
                    cancellationToken).ConfigureAwait(false);

            List<UserBestBeatmap> userBestBeatmap = new List<UserBestBeatmap>();
            List<UserBest> userBest = JsonConvert.DeserializeObject<List<UserBest>>(request);
            foreach (UserBest best in userBest)
                userBestBeatmap.Add(new UserBestBeatmap
                {
                    Beatmap = await GetBeatmapAsync(best.BeatmapId, BeatmapType.ByDifficulty, gameMode,
                        cancellationToken).ConfigureAwait(false),
                    UserBest = best
                });

            return userBestBeatmap;
        }

        /// <inheritdoc />
        public List<UserRecent> GetUserRecentByUsername(string username, GameMode gameMode = GameMode.Standard,
            int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("The given username was null or white space.", nameof(username));

            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_recent called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            string request =
                Get(
                    $"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}");

            return JsonConvert.DeserializeObject<List<UserRecent>>(request);
        }

        /// <inheritdoc />
        public async Task<List<UserRecent>> GetUserRecentByUsernameAsync(string username,
            GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            return await GetUserRecentByUsernameAsync(username, gameMode, limit, CancellationToken.None)
                .ConfigureAwait(false);
        }

        public async Task<List<UserRecent>> GetUserRecentByUsernameAsync(string username, GameMode gameMode, int limit,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("The given username was null or white space.", nameof(username));

            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_recent called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            string request =
                await GetAsync(
                    $"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}",
                    cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<UserRecent>>(request);
        }

        /// <inheritdoc />
        public List<UserRecentBeatmap> GetUserRecentAndBeatmapByUsername(string username,
            GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("The given username was null or white space.", nameof(username));

            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_recent called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            string request =
                Get(
                    $"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}");

            List<UserRecentBeatmap> userRecentBeatmap = new List<UserRecentBeatmap>();
            List<UserRecent> userRecents = JsonConvert.DeserializeObject<List<UserRecent>>(request);
            foreach (UserRecent recent in userRecents)
                userRecentBeatmap.Add(new UserRecentBeatmap
                {
                    Beatmap = GetBeatmap(recent.BeatmapId),
                    UserRecent = recent
                });

            return userRecentBeatmap;
        }

        /// <inheritdoc />
        public async Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUsernameAsync(string username,
            GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            return await GetUserRecentAndBeatmapByUsernameAsync(username, gameMode, limit, CancellationToken.None)
                .ConfigureAwait(false);
        }

        public async Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUsernameAsync(string username,
            GameMode gameMode, int limit, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("The given username was null or white space.", nameof(username));

            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_recent called: {GetNameValues(username, gameMode, limit)}", DateTime.Now);

            string request =
                await GetAsync(
                    $"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}",
                    cancellationToken).ConfigureAwait(false);

            List<UserRecentBeatmap> userRecentBeatmap = new List<UserRecentBeatmap>();
            List<UserRecent> userRecents = JsonConvert.DeserializeObject<List<UserRecent>>(request);
            foreach (UserRecent recent in userRecents)
                userRecentBeatmap.Add(new UserRecentBeatmap
                {
                    Beatmap = await GetBeatmapAsync(recent.BeatmapId, BeatmapType.ByDifficulty, gameMode,
                        cancellationToken).ConfigureAwait(false),
                    UserRecent = recent
                });

            return userRecentBeatmap;
        }

        /// <inheritdoc />
        public List<UserRecent> GetUserRecentByUserid(ulong userid, GameMode gameMode = GameMode.Standard,
            int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_recent called: {GetNameValues(userid, gameMode, limit)}", DateTime.Now);

            string request =
                Get(
                    $"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}");

            return JsonConvert.DeserializeObject<List<UserRecent>>(request);
        }

        /// <inheritdoc />
        public async Task<List<UserRecent>> GetUserRecentByUseridAsync(ulong userid,
            GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            return await GetUserRecentByUseridAsync(userid, gameMode, limit, CancellationToken.None)
                .ConfigureAwait(false);
        }

        public async Task<List<UserRecent>> GetUserRecentByUseridAsync(ulong userid, GameMode gameMode, int limit,
            CancellationToken cancellationToken)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_recent called: {GetNameValues(userid, gameMode, limit)}", DateTime.Now);

            string request =
                await GetAsync(
                    $"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}",
                    cancellationToken).ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<UserRecent>>(request);
        }

        /// <inheritdoc />
        public List<UserRecentBeatmap> GetUserRecentAndBeatmapByUserid(ulong userid,
            GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints",
                $"/api/get_user_recent called: {GetNameValues(userid, gameMode, limit)}", DateTime.Now);

            string request =
                Get(
                    $"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}");

            List<UserRecentBeatmap> userRecentBeatmap = new List<UserRecentBeatmap>();
            List<UserRecent> userRecents = JsonConvert.DeserializeObject<List<UserRecent>>(request);
            foreach (UserRecent recent in userRecents)
                userRecentBeatmap.Add(new UserRecentBeatmap
                {
                    Beatmap = GetBeatmap(recent.BeatmapId),
                    UserRecent = recent
                });

            return userRecentBeatmap;
        }

        /// <inheritdoc />
        public async Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUseridAsync(ulong userid,
            GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            return await GetUserRecentAndBeatmapByUseridAsync(userid, gameMode, limit, CancellationToken.None)
                .ConfigureAwait(false);
        }

        public async Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUseridAsync(ulong userid, GameMode gameMode,
            int limit, CancellationToken cancellationToken)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", "/api/get_user_recent called: ", DateTime.Now);

            string request =
                await GetAsync(
                    $"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}",
                    cancellationToken).ConfigureAwait(false);

            List<UserRecentBeatmap> userRecentBeatmap = new List<UserRecentBeatmap>();
            List<UserRecent> userRecents = JsonConvert.DeserializeObject<List<UserRecent>>(request);
            foreach (UserRecent recent in userRecents)
                userRecentBeatmap.Add(new UserRecentBeatmap
                {
                    Beatmap = await GetBeatmapAsync(recent.BeatmapId, BeatmapType.ByDifficulty, gameMode,
                        cancellationToken).ConfigureAwait(false),
                    UserRecent = recent
                });

            return userRecentBeatmap;
        }

        /// <inheritdoc />
        public Matchs GetMatch(ulong matchid)
        {
            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", "/api/get_match called: ", DateTime.Now);

            string request = Get($"{GET_MATCH_URL}{API_KEY_PARAMETER}{ApiKey}{MATCH_PARAMETER}{matchid}");

            List<Matchs> r = JsonConvert.DeserializeObject<List<Matchs>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public async Task<Matchs> GetMatchAsync(ulong matchid)
        {
            return await GetMatchAsync(matchid, CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<Matchs> GetMatchAsync(ulong matchid, CancellationToken cancellationToken)
        {
            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", "/api/get_match called:", DateTime.Now);

            string request =
                await GetAsync($"{GET_MATCH_URL}{API_KEY_PARAMETER}{ApiKey}{MATCH_PARAMETER}{matchid}",
                    cancellationToken).ConfigureAwait(false);

            List<Matchs> r = JsonConvert.DeserializeObject<List<Matchs>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public Replay GetReplayByUsername(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("The given username was null or white space.", nameof(username));

            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", "/api/get_replay called: ", DateTime.Now);

            string request =
                Get(
                    $"{GET_REPLAY_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{BEATMAP_PARAMETER}{beatmapid}{USER_PARAMETER}{username}");

            List<Replay> r = JsonConvert.DeserializeObject<List<Replay>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public async Task<Replay> GetReplayByUsernameAsync(ulong beatmapid, string username,
            GameMode gameMode = GameMode.Standard)
        {
            return await GetReplayByUsernameAsync(beatmapid, username, gameMode, CancellationToken.None)
                .ConfigureAwait(false);
        }

        public async Task<Replay> GetReplayByUsernameAsync(ulong beatmapid, string username, GameMode gameMode,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("The given username was null or white space.", nameof(username));

            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", "/api/get_replay called: ", DateTime.Now);

            string request =
                await GetAsync(
                    $"{GET_REPLAY_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{BEATMAP_PARAMETER}{beatmapid}{USER_PARAMETER}{username}",
                    cancellationToken).ConfigureAwait(false);

            List<Replay> r = JsonConvert.DeserializeObject<List<Replay>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public Replay GetReplayByUserid(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", "/api/get_replay called: ", DateTime.Now);

            string request =
                Get(
                    $"{GET_REPLAY_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{BEATMAP_PARAMETER}{beatmapid}{USER_PARAMETER}{userid}");

            List<Replay> r = JsonConvert.DeserializeObject<List<Replay>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public async Task<Replay> GetReplayByUseridAsync(ulong beatmapid, ulong userid,
            GameMode gameMode = GameMode.Standard)
        {
            return await GetReplayByUseridAsync(beatmapid, userid, gameMode, CancellationToken.None)
                .ConfigureAwait(false);
        }

        public async Task<Replay> GetReplayByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode,
            CancellationToken cancellationToken)
        {
            string mode = UserMode.ToString(gameMode);

            Logger.LogMessage(LoggingLevel.Debug, "Endpoints", "/api/get_replay called: ", DateTime.Now);

            string request =
                await GetAsync(
                    $"{GET_REPLAY_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{BEATMAP_PARAMETER}{beatmapid}{USER_PARAMETER}{userid}",
                    cancellationToken).ConfigureAwait(false);

            List<Replay> r = JsonConvert.DeserializeObject<List<Replay>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        private string Get(string url)
        {
            Limiter.HandleAsync().ConfigureAwait(false).GetAwaiter().GetResult();

            HttpResponseMessage message = _httpClient.GetAsync(ROOT_DOMAIN + url).Result;

            if (message.StatusCode == HttpStatusCode.OK) return message.Content.ReadAsStringAsync().Result;
            throw new OsuSharpException(message.Content.ReadAsStringAsync().Result);
        }

        private async Task<string> GetAsync(string url, CancellationToken cancellationToken)
        {
            await Limiter.HandleAsync(cancellationToken).ConfigureAwait(false);

            HttpResponseMessage message = await _httpClient.GetAsync(ROOT_DOMAIN + url, cancellationToken).ConfigureAwait(false);

            if (message.StatusCode == HttpStatusCode.OK)
                return await message.Content.ReadAsStringAsync().ConfigureAwait(false);
            throw new OsuSharpException(await message.Content.ReadAsStringAsync().ConfigureAwait(false));
        }

        private string GetNameValues(params object[] parameters)
        {
            StringBuilder builder = new StringBuilder();

            foreach (object param in parameters) builder.Append($"[{param.GetType().Name}: {param}] ");

            return builder.ToString();
        }
    }
}