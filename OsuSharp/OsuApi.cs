using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OsuSharp.BeatmapsEndpoint;
using OsuSharp.Common;
using OsuSharp.MatchEndpoint;
using OsuSharp.ReplayEndpoint;
using OsuSharp.ScoreEndpoint;
using OsuSharp.UserBestEndpoint;
using OsuSharp.UserEndpoint;

namespace OsuSharp
{
    public static class OsuApi
    {
        private const string RootDomain = "https://osu.ppy.sh";
        private const string GetBeatmapsUrl = "/api/get_beatmaps";
        private const string GetUserUrl = "/api/get_user";
        private const string GetScoresUrl = "/api/get_scores";
        private const string GetUserBestUrl = "/api/get_user_best";
        private const string GetUserRecentUrl = "/api/get_user_recent";
        private const string GetMatchUrl = "/api/get_match";
        private const string GetReplayUrl = "/api/get_replay";
        private const string ApiKeyParameter = "?k=";
        private const string UserParameter = "&u=";
        private const string MatchParameter = "&mp=";
        private const string LimitParameter = "&limit=";
        private const string BeatmapParameter = "&b=";

        public static string ApiKey { get; set; }

        public static void Init(string apiKey)
        {
            ApiKey = apiKey;
        }

        public static async Task<Beatmaps> GetBeatmapAsync(ulong beatmapId, BeatmapType bmType = BeatmapType.ByDifficulty, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserModeConverter.ConvertToString(gameMode);
            string type = BeatmapsType.BeatmapTypeConverter(bmType);
            string request = await GetAsync($"{RootDomain}{GetBeatmapsUrl}{ApiKeyParameter}{ApiKey}{type}{beatmapId}{mode}");
            List<Beatmaps> r = JsonConvert.DeserializeObject<List<Beatmaps>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<List<Beatmaps>> GetBeatmapsAsync(string nickname, BeatmapType bmType = BeatmapType.ByCreator, GameMode gameMode = GameMode.Standard, int limit = 500)
        {
            string mode = UserModeConverter.ConvertToString(gameMode);
            string type = BeatmapsType.BeatmapTypeConverter(bmType);
            string request = await GetAsync($"{RootDomain}{GetBeatmapsUrl}{ApiKeyParameter}{ApiKey}{type}{nickname}{LimitParameter}{limit}{mode}");
            return JsonConvert.DeserializeObject<List<Beatmaps>>(request);
        }

        public static async Task<List<Beatmaps>> GetBeatmapsAsync(ulong id, BeatmapType bmType = BeatmapType.ByBeatmap, GameMode gameMode = GameMode.Standard, int limit = 500)
        {
            string mode = UserModeConverter.ConvertToString(gameMode);
            string type = BeatmapsType.BeatmapTypeConverter(bmType);
            string request = await GetAsync($"{RootDomain}{GetBeatmapsUrl}{ApiKeyParameter}{ApiKey}{type}{id}{LimitParameter}{limit}{mode}");
            return JsonConvert.DeserializeObject<List<Beatmaps>>(request);
        }

        public static async Task<List<Beatmaps>> GetLastBeatmapsAsync(int limit = 500)
        {
            string request = await GetAsync($"{RootDomain}{GetBeatmapsUrl}{ApiKeyParameter}{ApiKey}{LimitParameter}{limit}");
            return JsonConvert.DeserializeObject<List<Beatmaps>>(request);
        }

        public static async Task<Users> GetUserByNameAsync(string username, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserModeConverter.ConvertToString(gameMode);
            string request = await GetAsync($"{RootDomain}{GetUserUrl}{ApiKeyParameter}{ApiKey}{UserParameter}{username}{mode}");
            List<Users> r = JsonConvert.DeserializeObject<List<Users>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<Users> GetUserByIdAsync(ulong userid, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserModeConverter.ConvertToString(gameMode);
            string request = await GetAsync($"{RootDomain}{GetUserUrl}{ApiKeyParameter}{ApiKey}{UserParameter}{userid}{mode}");
            List<Users> r = JsonConvert.DeserializeObject<List<Users>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<Scores> GetScoreByUsernameAsync(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserModeConverter.ConvertToString(gameMode);
            string request = await GetAsync($"{RootDomain}{GetScoresUrl}{ApiKeyParameter}{ApiKey}{mode}{UserParameter}{username}{BeatmapParameter}{beatmapid}");
            List<Scores> r = JsonConvert.DeserializeObject<List<Scores>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<Scores> GetScoreByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserModeConverter.ConvertToString(gameMode);
            string request = await GetAsync($"{RootDomain}{GetScoresUrl}{ApiKeyParameter}{ApiKey}{mode}{UserParameter}{userid}{BeatmapParameter}{beatmapid}");
            List<Scores> r = JsonConvert.DeserializeObject<List<Scores>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<List<Scores>> GetScoresAsync(ulong beatmapid, GameMode gameMode = GameMode.Standard, int limit = 50)
        {
            string mode = UserModeConverter.ConvertToString(gameMode);
            string request = await GetAsync($"{RootDomain}{GetScoresUrl}{ApiKeyParameter}{ApiKey}{mode}{LimitParameter}{limit}{BeatmapParameter}{beatmapid}");
            return JsonConvert.DeserializeObject<List<Scores>>(request);
        }

        public static async Task<List<UserBest>> GetUserBestByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserModeConverter.ConvertToString(gameMode);
            string request = await GetAsync($"{RootDomain}{GetUserBestUrl}{ApiKeyParameter}{ApiKey}{UserParameter}{username}{mode}{LimitParameter}{limit}");
            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        public static async Task<List<UserBest>> GetUserBestByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserModeConverter.ConvertToString(gameMode);
            string request = await GetAsync($"{RootDomain}{GetUserBestUrl}{ApiKeyParameter}{ApiKey}{UserParameter}{userid}{mode}{LimitParameter}{limit}");
            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        public static async Task<List<UserRecent.UserRecent>> GetUserRecentByUsernameAsync(string username, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserModeConverter.ConvertToString(gameMode);
            string request = await GetAsync($"{RootDomain}{GetUserRecentUrl}{ApiKeyParameter}{ApiKey}{UserParameter}{username}{mode}{LimitParameter}{limit}");
            return JsonConvert.DeserializeObject<List<UserRecent.UserRecent>>(request);
        }

        public static async Task<List<UserRecent.UserRecent>> GetUserRecentByUseridAsync(ulong userid, GameMode gameMode = GameMode.Standard, int limit = 10)
        {
            string mode = UserModeConverter.ConvertToString(gameMode);
            string request = await GetAsync($"{RootDomain}{GetUserRecentUrl}{ApiKeyParameter}{ApiKey}{UserParameter}{userid}{mode}{LimitParameter}{limit}");
            return JsonConvert.DeserializeObject<List<UserRecent.UserRecent>>(request);
        }

        public static async Task<Matchs> GetMatchAsync(ulong matchid)
        {
            string request = await GetAsync($"{RootDomain}{GetMatchUrl}{ApiKeyParameter}{ApiKey}{MatchParameter}{matchid}");
            List<Matchs> r = JsonConvert.DeserializeObject<List<Matchs>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<Replay> GetReplayByUsernameAsync(ulong beatmapid, string username, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserModeConverter.ConvertToString(gameMode);
            string request = await GetAsync($"{RootDomain}{GetReplayUrl}{ApiKeyParameter}{ApiKey}{mode}{BeatmapParameter}{beatmapid}{UserParameter}{username}");
            List<Replay> r = JsonConvert.DeserializeObject<List<Replay>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        public static async Task<Replay> GetReplayByUseridAsync(ulong beatmapid, ulong userid, GameMode gameMode = GameMode.Standard)
        {
            string mode = UserModeConverter.ConvertToString(gameMode);
            string request = await GetAsync($"{RootDomain}{GetReplayUrl}{ApiKeyParameter}{ApiKey}{mode}{BeatmapParameter}{beatmapid}{UserParameter}{userid}");
            List<Replay> r = JsonConvert.DeserializeObject<List<Replay>>(request);
            return r.Count > 0 ? r[0] : null;
        }

        private static async Task<string> GetAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(RootDomain);
                HttpResponseMessage message = await client.GetAsync(url);
                if (message.StatusCode == HttpStatusCode.OK)
                {
                    return await message.Content.ReadAsStringAsync();
                }
                throw new OsuSharpException(await message.Content.ReadAsStringAsync());
            }
        }
    }
}