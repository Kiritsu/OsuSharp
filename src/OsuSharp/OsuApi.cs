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
    public class OsuApi
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
        public static string ModsSeparator = "";

        public string ApiKey { get; internal set; }
        

        /// <summary>
        /// Method that initializes the library to perform your requests.
        /// </summary>
        /// <param name="apiKey">Token provided by osu!api.</param>
        public static OsuApi CreateInstance(string apiKey)
        {
            if (_httpClient == null)
            {
                _httpClient = new HttpClient { BaseAddress = new Uri(ROOT_DOMAIN) };
            }

            return new OsuApi
            {
                ApiKey = apiKey
            };
        }

        /// <summary>
        /// Sets the ModsSeparator.
        /// </summary>
        /// <param name="separator">Separator between each mod.</param>
        public static void SetGlobalModsSeparator(string separator)
        {
            ModsSeparator = separator;
        }

        /// <summary>
        /// Sets the ModsSeparator
        /// </summary>
        /// <param name="separator">Separator between each mod.</param>
        public static void SetGlobalModsSeparator(char separator)
        {
            ModsSeparator = separator.ToString();
        }

        /// <summary>
        /// Method that returns a <see cref="Beatmap"/>. It requires a valid BeatmapId.
        /// </summary>
        /// <param name="beatmapId">Id of the beatmap.</param>
        /// <param name="bmType">Type of the beatmap. Beatmapset or difficulty.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <returns></returns>
        public async Task<Beatmap> GetBeatmapAsync(ulong beatmapId, BeatmapType bmType = BeatmapType.ByDifficulty, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string type = BeatmapParam.ToString(bmType);
            string request = await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{beatmapId}{mode}");
            List<Beatmap> r = JsonConvert.DeserializeObject<List<Beatmap>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        /// Method that returns a list of <see cref="Beatmap"/> by the given creator's nickname.
        /// </summary>
        /// <param name="nickname">Author's nickname of the beatmap.</param>
        /// <param name="bmType">Type of the beatmap. ByCreator is required.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <returns></returns>
        public async Task<List<Beatmap>> GetBeatmapsAsync(string nickname, BeatmapType bmType = BeatmapType.ByCreator, GameMode gameMode = GameMode.Standard, int limit = 500)
        {
            string mode = UserMode.ToString(gameMode);
            string type = BeatmapParam.ToString(bmType);
            string request = await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{nickname}{LIMIT_PARAMETER}{limit}{mode}");
            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <summary>
        /// Method that returns a list of <see cref="Beatmap"/> by the given beatmapset id.
        /// </summary>
        /// <param name="id">Id of the beatmapset.</param>
        /// <param name="bmType">Type of the beatmap. ByBeatmap is required.</param>
        /// <param name="gameMode">Gamemode of the beatmap. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the ouput. Default and maximum : 500.</param>
        /// <returns></returns>
        public async Task<List<Beatmap>> GetBeatmapsAsync(ulong id, BeatmapType bmType = BeatmapType.ByBeatmap, GameMode gameMode = GameMode.Standard, int limit = 500)
        {
            string mode = UserMode.ToString(gameMode);
            string type = BeatmapParam.ToString(bmType);
            string request = await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{id}{LIMIT_PARAMETER}{limit}{mode}");
            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <summary>
        /// Method that returns a list of lasts uploaded <see cref="Beatmap"/>.
        /// </summary>
        /// <param name="limit">Limit of the output. Default and maximum : 500.</param>
        /// <returns></returns>
        public async Task<List<Beatmap>> GetLastBeatmapsAsync(int limit = 500)
        {
            string request = await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{LIMIT_PARAMETER}{limit}");
            return JsonConvert.DeserializeObject<List<Beatmap>>(request);
        }

        /// <summary>
        /// Method that returns a <see cref="User"/> by the given Username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <returns></returns>
        public async Task<User> GetUserByNameAsync(string username, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_USER_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}");
            List<User> r = JsonConvert.DeserializeObject<List<User>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        /// Method that returns a <see cref="User"/> by the given Userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the user. Standard, Taiko, Catch or Mania.</param>
        /// <returns></returns>
        public async Task<User> GetUserByIdAsync(ulong userid, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_USER_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}");
            List<User> r = JsonConvert.DeserializeObject<List<User>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        /// Method that returns a <see cref="Score"/> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns></returns>
        public async Task<Score> GetScoreByUsernameAsync(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{USER_PARAMETER}{username}{BEATMAP_PARAMETER}{beatmapid}");
            List<Score> r = JsonConvert.DeserializeObject<List<Score>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        /// Method that returns a <see cref="Score"/> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns></returns>
        public async Task<Score> GetScoreByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{USER_PARAMETER}{userid}{BEATMAP_PARAMETER}{beatmapid}");
            List<Score> r = JsonConvert.DeserializeObject<List<Score>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        /// Method that returns a list of <see cref="Score"/> by the given beatmapid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 50, minimum : 1, maximum : 100</param>
        /// <returns></returns>
        public async Task<List<Score>> GetScoresAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}");
            return JsonConvert.DeserializeObject<List<Score>>(request);
        }

        public async Task<BeatmapScores> GetScoresAndBeatmapAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}");
            List<Score> score = JsonConvert.DeserializeObject<List<Score>>(request);
            Beatmap beatmap = await GetBeatmapAsync(beatmapid, gameMode: gameMode);
            return new BeatmapScores
            {
                Beatmap = beatmap,
                Score = score
            };
        }

        public async Task<BeatmapScoresUsers> GetScoresWithUsersAndBeatmapAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}");
            List<Score> scores = JsonConvert.DeserializeObject<List<Score>>(request);
            List<User> users = new List<User>();
            Beatmap beatmap = await GetBeatmapAsync(beatmapid);
            foreach (Score score in scores)
            {
                users.Add(await GetUserByIdAsync(score.Userid, gameMode));
            }
            return new BeatmapScoresUsers
            {
                Beatmap = beatmap,
                Scores = scores,
                Users = users
            };
        }

        /// <summary>
        /// Method that returns a list of <see cref="UserBest"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns></returns>
        public async Task<List<UserBest>> GetUserBestByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}");

            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        public async Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}");
            List<UserBest> userBest = JsonConvert.DeserializeObject<List<UserBest>>(request);
            List<UserBestBeatmap> userBestBeatmap = new List<UserBestBeatmap>();
            foreach (UserBest best in userBest)
            {
                userBestBeatmap.Add(new UserBestBeatmap
                {
                    Beatmap = await GetBeatmapAsync(best.BeatmapId),
                    UserBest = best
                });
            }
            return userBestBeatmap;
        }

        /// <summary>
        /// Method that returns a list of <see cref="UserBest"/> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns></returns>
        public async Task<List<UserBest>> GetUserBestByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}");
            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        public async Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}");
            List<UserBest> userBest = JsonConvert.DeserializeObject<List<UserBest>>(request);
            List<UserBestBeatmap> userBestBeatmap = new List<UserBestBeatmap>();
            foreach (UserBest best in userBest)
            {
                userBestBeatmap.Add(new UserBestBeatmap
                {
                    Beatmap = await GetBeatmapAsync(best.BeatmapId),
                    UserBest = best
                });
            }
            return userBestBeatmap;
        }

        /// <summary>
        /// Method that returns a list of <see cref="UserRecent"/> by the given username.
        /// </summary>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns></returns>
        public async Task<List<UserRecent>> GetUserRecentByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}");
            return JsonConvert.DeserializeObject<List<UserRecent>>(request);
        }

        public async Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}");
            List<UserRecent> userRecents = JsonConvert.DeserializeObject<List<UserRecent>>(request);
            List<UserRecentBeatmap> userRecentBeatmap = new List<UserRecentBeatmap>();
            foreach (UserRecent recent in userRecents)
            {
                userRecentBeatmap.Add(new UserRecentBeatmap
                {
                    Beatmap = await GetBeatmapAsync(recent.BeatmapId),
                    UserRecent = recent
                });
            }
            return userRecentBeatmap;
        }

        /// <summary>
        /// Method that returns a list of <see cref="UserRecent"/> by the given userid.
        /// </summary>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <param name="limit">Limit of the output. Default : 10, minimum : 1, maximum : 100</param>
        /// <returns></returns>
        public async Task<List<UserRecent>> GetUserRecentByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}");
            return JsonConvert.DeserializeObject<List<UserRecent>>(request);
        }

        public async Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}");
            List<UserRecent> userRecents = JsonConvert.DeserializeObject<List<UserRecent>>(request);
            List<UserRecentBeatmap> userRecentBeatmap = new List<UserRecentBeatmap>();
            foreach (UserRecent recent in userRecents)
            {
                userRecentBeatmap.Add(new UserRecentBeatmap
                {
                    Beatmap = await GetBeatmapAsync(recent.BeatmapId),
                    UserRecent = recent
                });
            }
            return userRecentBeatmap;
        }

        /// <summary>
        /// Method that returns a <see cref="Matchs"/> by the given matchid.
        /// </summary>
        /// <param name="matchid">Id of the match.</param>
        /// <returns></returns>
        public async Task<Matchs> GetMatchAsync(ulong matchid)
        {
            string request = await GetAsync($"{GET_MATCH_URL}{API_KEY_PARAMETER}{ApiKey}{MATCH_PARAMETER}{matchid}");
            List<Matchs> r = JsonConvert.DeserializeObject<List<Matchs>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        /// Method that returns a <see cref="Replay"/> by the given beatmapid and username.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="username">Username of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns></returns>
        public async Task<Replay> GetReplayByUsernameAsync(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_REPLAY_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{BEATMAP_PARAMETER}{beatmapid}{USER_PARAMETER}{username}");
            List<Replay> r = JsonConvert.DeserializeObject<List<Replay>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        /// <summary>
        /// Method that returns a <see cref="Replay"/> by the given beatmapid and userid.
        /// </summary>
        /// <param name="beatmapid">Id of the beatmap. Must be the id of a difficulty.</param>
        /// <param name="userid">Id of the user.</param>
        /// <param name="gameMode">Gamemode of the play. Standard, Taiko, Catch or Mania.</param>
        /// <returns></returns>
        public async Task<Replay> GetReplayByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_REPLAY_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{BEATMAP_PARAMETER}{beatmapid}{USER_PARAMETER}{userid}");
            List<Replay> r = JsonConvert.DeserializeObject<List<Replay>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        private static async Task<string> GetAsync(string url)
        {
            HttpResponseMessage message = await _httpClient.GetAsync(url);
            if (message.StatusCode == HttpStatusCode.OK)
            {
                return await message.Content.ReadAsStringAsync();
            }
            throw new OsuSharpException(await message.Content.ReadAsStringAsync());
        }
    }
}