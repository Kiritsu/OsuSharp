using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.UserEndpoint
{
    public class Users
    {
        [JsonProperty("user_id")]
        public long Userid { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("playcount")]
        public uint PlayCount { get; set; }

        [JsonProperty("accuracy")]
        public double Accuracy { get; set; }

        [JsonProperty("pp_rank")]
        public uint GlobalRank { get; set; }

        [JsonProperty("pp_raw")]
        public float Pp { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("pp_country_rank")]
        public uint RegionalRank { get; set; }

        [JsonProperty("level")]
        public float Level { get; set; }

        [JsonProperty("total_score")]
        public ulong TotalScore { get; set; }

        [JsonProperty("ranked_score")]
        public ulong RankedScore { get; set; }

        [JsonProperty("count300")]
        public uint Count300 { get; set; }

        [JsonProperty("count100")]
        public uint Count100 { get; set; }

        [JsonProperty("count50")]
        public uint Count50 { get; set; }

        [JsonProperty("count_rank_ss")]
        public int SsRank { get; set; }

        [JsonProperty("count_rank_s")]
        public int SRank { get; set; }

        [JsonProperty("count_rank_a")]
        public int ARank { get; set; }

        [JsonProperty("events")]
        public List<Events> Events { get; set; }
    }
}