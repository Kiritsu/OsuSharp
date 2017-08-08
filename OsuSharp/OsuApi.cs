using Newtonsoft.Json;
using OsuSharp.BeatmapsEndpoint;
using OsuSharp.Common;
using OsuSharp.MatchEndpoint;
using OsuSharp.ReplayEndpoint;
using OsuSharp.ScoreEndpoint;
using OsuSharp.UserBestEndpoint;
using OsuSharp.UserEndpoint;
using OsuSharp.UserRecentEndpoint;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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
        private const string apiKeyParameter = "?k=";
        private const string userParameter = "&u=";
        private const string matchParameter = "&mp=";
        private const string limitParameter = "&limit=";
        private const string beatmapParameter = "&b=";

        public static string ApiKey { get; set; }

        public static void Init(string _apiKey)
        {
            ApiKey = _apiKey;
        }

        public static async Task<Beatmaps> GetBeatmapAsync(ulong beatmapId, BeatmapType bmType = BeatmapType.ByDifficulty, OsuMode oMode = OsuMode.Standard)
        {
            string mode = UserModeConverter.ConvertToString(oMode);
            string type = BeatmapsType.BeatmapTypeConverter(bmType);
            string request = await GetAsync($"{RootDomain}{GetBeatmapsUrl}{apiKeyParameter}{ApiKey}{type}{beatmapId}{mode}");
            var r = JsonConvert.DeserializeObject<List<Beatmaps>>(request);
            if (r.Count > 0)
            {
                return r[0];
            }
            return null;
        }

        public static async Task<List<Beatmaps>> GetBeatmapsAsync(string nickname, BeatmapType bmType = BeatmapType.ByCreator, OsuMode oMode = OsuMode.Standard, int limit = 500)
        {
            string mode = UserModeConverter.ConvertToString(oMode);
            string type = BeatmapsType.BeatmapTypeConverter(bmType);
            string request = await GetAsync($"{RootDomain}{GetBeatmapsUrl}{apiKeyParameter}{ApiKey}{type}{nickname}{limitParameter}{limit}{mode}");
            return JsonConvert.DeserializeObject<List<Beatmaps>>(request);
        }

        public static async Task<List<Beatmaps>> GetBeatmapsAsync(ulong id, BeatmapType bmType = BeatmapType.ByBeatmap, OsuMode oMode = OsuMode.Standard, int limit = 500)
        {
            string mode = UserModeConverter.ConvertToString(oMode);
            string type = BeatmapsType.BeatmapTypeConverter(bmType);
            string request = await GetAsync($"{RootDomain}{GetBeatmapsUrl}{apiKeyParameter}{ApiKey}{type}{id}{limitParameter}{limit}{mode}");
            return JsonConvert.DeserializeObject<List<Beatmaps>>(request);
        }

        public static async Task<List<Beatmaps>> GetLastBeatmapsAsync(int limit = 500)
        {
            string request = await GetAsync($"{RootDomain}{GetBeatmapsUrl}{apiKeyParameter}{ApiKey}{limitParameter}{limit}");
            return JsonConvert.DeserializeObject<List<Beatmaps>>(request);
        }

        public static async Task<Users> GetUserByNameAsync(string username, OsuMode oMode = OsuMode.Standard)
        {
            string mode = UserModeConverter.ConvertToString(oMode);
            string request = await GetAsync($"{RootDomain}{GetUserUrl}{apiKeyParameter}{ApiKey}{userParameter}{username}{mode}");
            var r = JsonConvert.DeserializeObject<List<Users>>(request);
            if (r.Count > 0)
            {
                return r[0];
            }
            return null;
        }

        public static async Task<Users> GetUserByIdAsync(ulong userid, OsuMode oMode = OsuMode.Standard)
        {
            string mode = UserModeConverter.ConvertToString(oMode);
            string request = await GetAsync($"{RootDomain}{GetUserUrl}{apiKeyParameter}{ApiKey}{userParameter}{userid}{mode}");
            var r = JsonConvert.DeserializeObject<List<Users>>(request);
            if (r.Count > 0)
            {
                return r[0];
            }
            return null;
        }

        public static async Task<Scores> GetScoreByUsernameAsync(ulong beatmapid, string username, OsuMode oMode = OsuMode.Standard)
        {
            string mode = UserModeConverter.ConvertToString(oMode);
            string request = await GetAsync($"{RootDomain}{GetScoresUrl}{apiKeyParameter}{ApiKey}{mode}{userParameter}{username}{beatmapParameter}{beatmapid}");
            var r = JsonConvert.DeserializeObject<List<Scores>>(request);
            if (r.Count > 0)
            {
                return r[0];
            }
            return null;
        }

        public static async Task<Scores> GetScoreByUseridAsync(ulong beatmapid, ulong userid, OsuMode oMode = OsuMode.Standard)
        {
            string mode = UserModeConverter.ConvertToString(oMode);
            string request = await GetAsync($"{RootDomain}{GetScoresUrl}{apiKeyParameter}{ApiKey}{mode}{userParameter}{userid}{beatmapParameter}{beatmapid}");
            var r = JsonConvert.DeserializeObject<List<Scores>>(request);
            if (r.Count > 0)
            {
                return r[0];
            }
            return null;
        }

        public static async Task<List<Scores>> GetScoresAsync(ulong beatmapid, OsuMode oMode = OsuMode.Standard, int limit = 50)
        {
            string mode = UserModeConverter.ConvertToString(oMode);
            string request = await GetAsync($"{RootDomain}{GetScoresUrl}{apiKeyParameter}{ApiKey}{mode}{limitParameter}{limit}{beatmapParameter}{beatmapid}");
            return JsonConvert.DeserializeObject<List<Scores>>(request);
        }

        public static async Task<List<UserBest>> GetUserBestByUsernameAsync(string username, OsuMode oMode = OsuMode.Standard, int limit = 10)
        {
            string mode = UserModeConverter.ConvertToString(oMode);
            string request = await GetAsync($"{RootDomain}{GetUserBestUrl}{apiKeyParameter}{ApiKey}{userParameter}{username}{mode}{limitParameter}{limit}");
            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        public static async Task<List<UserBest>> GetUserBestByUseridAsync(ulong userid, OsuMode oMode = OsuMode.Standard, int limit = 10)
        {
            string mode = UserModeConverter.ConvertToString(oMode);
            string request = await GetAsync($"{RootDomain}{GetUserBestUrl}{apiKeyParameter}{ApiKey}{userParameter}{userid}{mode}{limitParameter}{limit}");
            return JsonConvert.DeserializeObject<List<UserBest>>(request);
        }

        public static async Task<List<UserRecent>> GetUserRecentByUsernameAsync(string username, OsuMode oMode = OsuMode.Standard, int limit = 10)
        {
            string mode = UserModeConverter.ConvertToString(oMode);
            string request = await GetAsync($"{RootDomain}{GetUserRecentUrl}{apiKeyParameter}{ApiKey}{userParameter}{username}{mode}{limitParameter}{limit}");
            return JsonConvert.DeserializeObject<List<UserRecent>>(request);
        }

        public static async Task<List<UserRecent>> GetUserRecentByUseridAsync(ulong userid, OsuMode oMode = OsuMode.Standard, int limit = 10)
        {
            string mode = UserModeConverter.ConvertToString(oMode);
            string request = await GetAsync($"{RootDomain}{GetUserRecentUrl}{apiKeyParameter}{ApiKey}{userParameter}{userid}{mode}{limitParameter}{limit}");
            return JsonConvert.DeserializeObject<List<UserRecent>>(request);
        }

        public static async Task<Matchs> GetMatchAsync(ulong matchid)
        {
            string request = await GetAsync($"{RootDomain}{GetMatchUrl}{apiKeyParameter}{ApiKey}{matchParameter}{matchid}");
            var r = JsonConvert.DeserializeObject<List<Matchs>>(request);
            if (r.Count > 0)
            {
                return r[0];
            }
            return null;
        }

        public static async Task<Replay> GetReplayByUsernameAsync(ulong beatmapid, string username, OsuMode oMode = OsuMode.Standard)
        {
            string mode = UserModeConverter.ConvertToString(oMode);
            string request = await GetAsync($"{RootDomain}{GetReplayUrl}{apiKeyParameter}{ApiKey}{mode}{beatmapParameter}{beatmapid}{userParameter}{username}");
            var r = JsonConvert.DeserializeObject<List<Replay>>(request);
            if (r.Count > 0)
            {
                return r[0];
            }
            return null;
        }

        public static async Task<Replay> GetReplayByUseridAsync(ulong beatmapid, ulong userid, OsuMode oMode = OsuMode.Standard)
        {
            string mode = UserModeConverter.ConvertToString(oMode);
            string request = await GetAsync($"{RootDomain}{GetReplayUrl}{apiKeyParameter}{ApiKey}{mode}{beatmapParameter}{beatmapid}{userParameter}{userid}");
            var r = JsonConvert.DeserializeObject<List<Replay>>(request);
            if (r.Count > 0)
            {
                return r[0];
            }
            return null;
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
                else
                {
                    throw new OsuSharpException(await message.Content.ReadAsStringAsync());
                }
            }
        }
    }
}