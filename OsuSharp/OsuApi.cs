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
    public static class OsuApi
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

        public static string ApiKey { get; internal set; }
        public static string ModsSeparator { get; internal set; }

        public static void Init(string apiKey, string modsSeparator = "")
        {
            ModsSeparator = modsSeparator;
            ApiKey = apiKey;
            _httpClient = new HttpClient {BaseAddress = new Uri(ROOT_DOMAIN)};
        }

        public static async Task<Beatmaps> GetBeatmapAsync(ulong beatmapId, BeatmapType bmType = BeatmapType.ByDifficulty, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string type = Beatmap.ToString(bmType);
            string request = await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{beatmapId}{mode}");
            List<Beatmaps> r = JsonConvert.DeserializeObject<List<Beatmaps>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<List<Beatmaps>> GetBeatmapsAsync(string nickname, BeatmapType bmType = BeatmapType.ByCreator, GameMode gameMode = GameMode.Standard, int limit = 500)
        {
            string mode = UserMode.ToString(gameMode);
            string type = Beatmap.ToString(bmType);
            string request = await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{nickname}{LIMIT_PARAMETER}{limit}{mode}");
            return JsonConvert.DeserializeObject<List<Beatmaps>>(request);
        }

        public static async Task<List<Beatmaps>> GetBeatmapsAsync(ulong id, BeatmapType bmType = BeatmapType.ByBeatmap, GameMode gameMode = GameMode.Standard, int limit = 500)
        {
            string mode = UserMode.ToString(gameMode);
            string type = Beatmap.ToString(bmType);
            string request = await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{type}{id}{LIMIT_PARAMETER}{limit}{mode}");
            return JsonConvert.DeserializeObject<List<Beatmaps>>(request);
        }

        public static async Task<List<Beatmaps>> GetLastBeatmapsAsync(int limit = 500)
        {
            string request = await GetAsync($"{GET_BEATMAPS_URL}{API_KEY_PARAMETER}{ApiKey}{LIMIT_PARAMETER}{limit}");
            return JsonConvert.DeserializeObject<List<Beatmaps>>(request);
        }

        public static async Task<Users> GetUserByNameAsync(string username, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_USER_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}");
            List<Users> r = JsonConvert.DeserializeObject<List<Users>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<Users> GetUserByIdAsync(ulong userid, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_USER_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}");
            List<Users> r = JsonConvert.DeserializeObject<List<Users>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<Scores> GetScoreByUsernameAsync(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{USER_PARAMETER}{username}{BEATMAP_PARAMETER}{beatmapid}");
            List<Scores> r = JsonConvert.DeserializeObject<List<Scores>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<Scores> GetScoreByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{USER_PARAMETER}{userid}{BEATMAP_PARAMETER}{beatmapid}");
            List<Scores> r = JsonConvert.DeserializeObject<List<Scores>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<List<Scores>> GetScoresAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}");
            return JsonConvert.DeserializeObject<List<Scores>>(request);
        }

        public static async Task<BeatmapScores> GetScoresAndBeatmapAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}");
            List<Scores> score = JsonConvert.DeserializeObject<List<Scores>>(request);
            Beatmaps beatmap = await GetBeatmapAsync(beatmapid, gameMode: gameMode);
            return new BeatmapScores { Beatmap = beatmap, Score = score };
        }

        public static async Task<BeatmapScoresUsers> GetScoresWithUsersAndBeatmapAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_SCORES_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{LIMIT_PARAMETER}{limit}{BEATMAP_PARAMETER}{beatmapid}");
            List<Scores> scores = JsonConvert.DeserializeObject<List<Scores>>(request);
            List<Users> users = new List<Users>();
            Beatmaps beatmap = await GetBeatmapAsync(beatmapid);
            foreach (Scores score in scores)
            {
                users.Add(await GetUserByIdAsync(score.Userid, gameMode));
            }
            return new BeatmapScoresUsers { Beatmap = beatmap, Scores = scores, Users = users };
        }

        public static async Task<List<UserBest>> GetUserBestByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}");

            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        public static async Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10)
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

        public static async Task<List<UserBest>> GetUserBestByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_USER_BEST_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}");
            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        public static async Task<List<UserBestBeatmap>> GetUserBestAndBeatmapByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10)
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

        public static async Task<List<UserRecent>> GetUserRecentByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{username}{mode}{LIMIT_PARAMETER}{limit}");
            return JsonConvert.DeserializeObject<List<UserRecent>>(request);
        }

        public static async Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10)
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

        public static async Task<List<UserRecent>> GetUserRecentByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_USER_RECENT_URL}{API_KEY_PARAMETER}{ApiKey}{USER_PARAMETER}{userid}{mode}{LIMIT_PARAMETER}{limit}");
            return JsonConvert.DeserializeObject<List<UserRecent>>(request);
        }

        public static async Task<List<UserRecentBeatmap>> GetUserRecentAndBeatmapByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10)
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

        public static async Task<Matchs> GetMatchAsync(ulong matchid)
        {
            string request = await GetAsync($"{GET_MATCH_URL}{API_KEY_PARAMETER}{ApiKey}{MATCH_PARAMETER}{matchid}");
            List<Matchs> r = JsonConvert.DeserializeObject<List<Matchs>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<Replay> GetReplayByUsernameAsync(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserMode.ToString(gameMode);
            string request = await GetAsync($"{GET_REPLAY_URL}{API_KEY_PARAMETER}{ApiKey}{mode}{BEATMAP_PARAMETER}{beatmapid}{USER_PARAMETER}{username}");
            List<Replay> r = JsonConvert.DeserializeObject<List<Replay>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<Replay> GetReplayByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard)
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