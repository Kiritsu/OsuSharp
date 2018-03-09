using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.UserEndpoint
{
    public class User
    {
        [JsonProperty("user_id")]
        public long Userid { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("playcount")]
        public int PlayCount { get; set; }

        [JsonProperty("accuracy")]
        public double Accuracy { get; set; }

        [JsonProperty("pp_rank")]
        public int GlobalRank { get; set; }

        [JsonProperty("pp_raw")]
        public float Pp { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("pp_country_rank")]
        public int RegionalRank { get; set; }

        [JsonProperty("level")]
        public float Level { get; set; }

        [JsonProperty("total_score")]
        public long TotalScore { get; set; }

        [JsonProperty("ranked_score")]
        public long RankedScore { get; set; }

        [JsonProperty("count300")]
        public int Count300 { get; set; }

        [JsonProperty("count100")]
        public int Count100 { get; set; }

        [JsonProperty("count50")]
        public int Count50 { get; set; }

        [JsonProperty("count_rank_ss")]
        public int SsRank { get; set; }

        [JsonProperty("count_rank_s")]
        public int SRank { get; set; }

        [JsonProperty("count_rank_a")]
        public int ARank { get; set; }

        [JsonProperty("events")]
        public List<Event> Events { get; set; }
    }
}