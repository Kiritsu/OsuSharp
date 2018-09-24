using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.Endpoints
{
    public class User
    {
        /// <summary>
        ///     Id of the player
        /// </summary>
        [JsonProperty("user_id")]
        public long Userid { get; set; }

        /// <summary>
        ///     Username of the player
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        ///     Playcount of the player
        /// </summary>
        [JsonProperty("playcount")]
        public int PlayCount { get; set; }

        /// <summary>
        ///     Main accuracy of the player
        /// </summary>
        [JsonProperty("accuracy")]
        public double Accuracy { get; set; }

        /// <summary>
        ///     Global rank of the player
        /// </summary>
        [JsonProperty("pp_rank")]
        public int GlobalRank { get; set; }

        /// <summary>
        ///     PP amount of the player
        /// </summary>
        [JsonProperty("pp_raw")]
        public float Pp { get; set; }

        /// <summary>
        ///     Country of the account
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        ///     Local rank of the player
        /// </summary>
        [JsonProperty("pp_country_rank")]
        public int RegionalRank { get; set; }

        /// <summary>
        ///     Level of the player
        /// </summary>
        [JsonProperty("level")]
        public float Level { get; set; }

        /// <summary>
        ///     Total score submitted for this player
        /// </summary>
        [JsonProperty("total_score")]
        public long TotalScore { get; set; }

        /// <summary>
        ///     Total ranked score submitted for this player
        /// </summary>
        [JsonProperty("ranked_score")]
        public long RankedScore { get; set; }

        /// <summary>
        ///     Count of 300
        /// </summary>
        [JsonProperty("count300")]
        public int Count300 { get; set; }

        /// <summary>
        ///     Count of 100
        /// </summary>
        [JsonProperty("count100")]
        public int Count100 { get; set; }

        /// <summary>
        ///     Count of 50
        /// </summary>
        [JsonProperty("count50")]
        public int Count50 { get; set; }

        /// <summary>
        ///     Count of SS ranks
        /// </summary>
        [JsonProperty("count_rank_ss")]
        public int SsRank { get; set; }

        /// <summary>
        ///     Count of S ranks
        /// </summary>
        [JsonProperty("count_rank_s")]
        public int SRank { get; set; }

        /// <summary>
        ///     Count of A ranks
        /// </summary>
        [JsonProperty("count_rank_a")]
        public int ARank { get; set; }

        /// <summary>
        ///     Events list of the player
        /// </summary>
        [JsonProperty("events")]
        public List<Event> Events { get; set; }
    }
}