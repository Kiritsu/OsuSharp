using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OsuSharp.BeatmapsEndpoint;
using OsuSharp.Entities;
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
        public string ModsSeparator = "";

        /// <inheritdoc />
        public string ApiKey { get; internal set; }

        /// <summary>
        /// Method that initializes the library to perform your requests.
        /// </summary>
        /// <param name="apiKey">Token provided by osu!api.</param>
        /// <param name="modsSeparator">String separator of displayed mods in Mods property.</param>
        /// <param name="httpClient">Custom instance of <see cref="HttpClient"/> (by default, if there are no instance of it, it will create one)</param>
        public static OsuApi CreateInstance(string apiKey, string modsSeparator = "", HttpClient httpClient = null)
        {
            if (_httpClient == null && httpClient == null)
            {
                _httpClient = new HttpClient();
            }
            else if (httpClient != null)
            {
                _httpClient = httpClient;
            }

            return new OsuApi
            {
                ApiKey = apiKey,
                ModsSeparator = modsSeparator
            };
        }

        public void SetModsSeparator(string modSeparator)
        {
            ModsSeparator = modSeparator;
        }

        public void SetModsSeparator(char modSeparator)
        {
            ModsSeparator = modSeparator.ToString();
        }

        /// <inheritdoc />
        public async Task<Beatmap> GetBeatmapAsync(ulong beatmapId, BeatmapType bmType = BeatmapType.ByDifficulty, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string type = BeatmapParam.ToString(bmType);

            string request = await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{beatmapId}{mode}").ConfigureAwait(false);

            List<Beatmap> r = JsonConvert.DeserializeObject<List<Beatmap>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public async Task<List<Beatmap>> GetBeatmapsAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 500)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            string mode = UserMode.ToString(gameMode);
            string type = BeatmapParam.ToString(BeatmapType.ByCreator);

            string request = await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{username}{LIMIT_PARAMETER}{limit}{mode}").ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <inheritdoc />
        public async Task<List<Beatmap>> GetBeatmapsAsync(ulong id, BeatmapType bmType = BeatmapType.ByBeatmap, GameMode gameMode = GameMode.Standard, int limit = 500)
        {
            string mode = UserMode.ToString(gameMode);
            string type = BeatmapParam.ToString(bmType);

            string request = await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{id}{LIMIT_PARAMETER}{limit}{mode}").ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <inheritdoc />
        public async Task<List<Beatmap>> GetLastBeatmapsAsync(int limit = 500)
        {
            string request = await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{LIMIT_PARAMETER}{limit}").ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <inheritdoc />
        public async Task<User> GetUserByNameAsync(string username, GameMode gameMode = GameMode.Standard)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            string mode = UserMode.ToString(gameMode);

            string request = await GetAsync($"{GET_USER_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}").ConfigureAwait(false);

            List<User> r = JsonConvert.DeserializeObject<List<User>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public async Task<User> GetUserByIdAsync(ulong userid, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);

            string request = await GetAsync($"{GET_USER_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}").ConfigureAwait(false);

            List<User> r = JsonConvert.DeserializeObject<List<User>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public async Task<Score> GetScoreByUsernameAsync(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            string mode = UserMode.ToString(gameMode);

            string request = await GetAsync($"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{USER_PARAMETER}{username}{BEATMAP_PARAMETER}{beatmapid}").ConfigureAwait(false);

            List<Score> r = JsonConvert.DeserializeObject<List<Score>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public async Task<Score> GetScoreByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);

            string request = await GetAsync($"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{USER_PARAMETER}{userid}{BEATMAP_PARAMETER}{beatmapid}").ConfigureAwait(false);

            List<Score> r = JsonConvert.DeserializeObject<List<Score>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public async Task<List<Score>> GetScoresAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50)
        {
            string mode = UserMode.ToString(gameMode);

            string request = await GetAsync($"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}").ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<Score>>(request);
        }

        /// <inheritdoc />
        public async Task<BeatmapScores> GetScoresAndBeatmapAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50)
        {
            string mode = UserMode.ToString(gameMode);

            string request = await GetAsync($"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}").ConfigureAwait(false);
            Beatmap beatmap = await GetBeatmapAsync(beatmapid, gameMode: gameMode).ConfigureAwait(false);

            List<Score> score = JsonConvert.DeserializeObject<List<Score>>(request);
            return new BeatmapScores
            {
                Beatmap = beatmap,
                Score = score
            };
        }

        /// <inheritdoc />
        public async Task<BeatmapScoresUsers> GetScoresWithUsersAndBeatmapAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50)
        {
            string mode = UserMode.ToString(gameMode);

            string request = await GetAsync($"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}").ConfigureAwait(false);
            Beatmap beatmap = await GetBeatmapAsync(beatmapid).ConfigureAwait(false);

            List<User> users = new List<User>();
            List<Score> scores = JsonConvert.DeserializeObject<List<Score>>(request);
            foreach (Score score in scores)
            {
                users.Add(await GetUserByIdAsync(score.Userid, gameMode).ConfigureAwait(false));
            }

            return new BeatmapScoresUsers
            {
                Beatmap = beatmap,
                Scores = scores,
                Users = users
            };
        }

        /// <inheritdoc />
        public async Task<List<UserBest>> GetUserBestByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            string mode = UserMode.ToString(gameMode);

            string request = await GetAsync($"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}").ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        /// <inheritdoc />
        public async Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            string mode = UserMode.ToString(gameMode);

            string request = await GetAsync($"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}").ConfigureAwait(false);

            List<UserBestBeatmap> userBestBeatmap = new List<UserBestBeatmap>();
            List<UserBest> userBest = JsonConvert.DeserializeObject<List<UserBest>>(request);
            foreach (UserBest best in userBest)
            {
                userBestBeatmap.Add(new UserBestBeatmap
                {
                    Beatmap = await GetBeatmapAsync(best.BeatmapId).ConfigureAwait(false),
                    UserBest = best
                });
            }

            return userBestBeatmap;
        }

        /// <inheritdoc />
        public async Task<List<UserBest>> GetUserBestByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);

            string request = await GetAsync($"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}").ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        /// <inheritdoc />
        public async Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);

            string request = await GetAsync($"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}").ConfigureAwait(false);

            List<UserBestBeatmap> userBestBeatmap = new List<UserBestBeatmap>();
            List<UserBest> userBest = JsonConvert.DeserializeObject<List<UserBest>>(request);
            foreach (UserBest best in userBest)
            {
                userBestBeatmap.Add(new UserBestBeatmap
                {
                    Beatmap = await GetBeatmapAsync(best.BeatmapId).ConfigureAwait(false),
                    UserBest = best
                });
            }

            return userBestBeatmap;
        }

        /// <inheritdoc />
        public async Task<List<UserRecent>> GetUserRecentByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            string mode = UserMode.ToString(gameMode);

            string request = await GetAsync($"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}").ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<UserRecent>>(request);
        }

        /// <inheritdoc />
        public async Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            string mode = UserMode.ToString(gameMode);

            string request = await GetAsync($"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}").ConfigureAwait(false);

            List<UserRecentBeatmap> userRecentBeatmap = new List<UserRecentBeatmap>();
            List<UserRecent> userRecents = JsonConvert.DeserializeObject<List<UserRecent>>(request);
            foreach (UserRecent recent in userRecents)
            {
                userRecentBeatmap.Add(new UserRecentBeatmap
                {
                    Beatmap = await GetBeatmapAsync(recent.BeatmapId).ConfigureAwait(false),
                    UserRecent = recent
                });
            }

            return userRecentBeatmap;
        }

        /// <inheritdoc />
        public async Task<List<UserRecent>> GetUserRecentByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);

            string request = await GetAsync($"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}").ConfigureAwait(false);

            return JsonConvert.DeserializeObject<List<UserRecent>>(request);
        }

        /// <inheritdoc />
        public async Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);

            string request = await GetAsync($"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}").ConfigureAwait(false);

            List<UserRecentBeatmap> userRecentBeatmap = new List<UserRecentBeatmap>();
            List<UserRecent> userRecents = JsonConvert.DeserializeObject<List<UserRecent>>(request);
            foreach (UserRecent recent in userRecents)
            {
                userRecentBeatmap.Add(new UserRecentBeatmap
                {
                    Beatmap = await GetBeatmapAsync(recent.BeatmapId).ConfigureAwait(false),
                    UserRecent = recent
                });
            }

            return userRecentBeatmap;
        }

        /// <inheritdoc />
        public async Task<Matchs> GetMatchAsync(ulong matchid)
        {
            string request = await GetAsync($"{GET_MATCH_URL}{API_KEY_PARAMETER}{ApiKey}{MATCH_PARAMETER}{matchid}").ConfigureAwait(false);

            List<Matchs> r = JsonConvert.DeserializeObject<List<Matchs>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public async Task<Replay> GetReplayByUsernameAsync(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("The given username was null or white space.", nameof(username));
            }

            string mode = UserMode.ToString(gameMode);

            string request = await GetAsync($"{GET_REPLAY_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{BEATMAP_PARAMETER}{beatmapid}{USER_PARAMETER}{username}").ConfigureAwait(false);

            List<Replay> r = JsonConvert.DeserializeObject<List<Replay>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <inheritdoc />
        public async Task<Replay> GetReplayByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);

            string request = await GetAsync($"{GET_REPLAY_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{BEATMAP_PARAMETER}{beatmapid}{USER_PARAMETER}{userid}").ConfigureAwait(false);

            List<Replay> r = JsonConvert.DeserializeObject<List<Replay>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        private static async Task<string> GetAsync(string url)
        {
            HttpResponseMessage message = await _httpClient.GetAsync(ROOT_DOMAIN + url).ConfigureAwait(false);

            if (message.StatusCode == HttpStatusCode.OK)
            {
                return await message.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            throw new OsuSharpException(await message.Content.ReadAsStringAsync().ConfigureAwait(false));
        }
    }
}