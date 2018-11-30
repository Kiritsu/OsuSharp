using System.Collections.Generic;
using Newtonsoft.Json;
using OsuSharp.Enums;

namespace OsuSharp.Endpoints
{
    public sealed class User : Endpoint
    {
        /// <summary>
        ///     Id of the player
        /// </summary>
        [JsonProperty("user_id")]
        public long Userid { get; internal set; }

        /// <summary>
        ///     Username of the player
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; internal set; }

        /// <summary>
        ///     Playcount of the player
        /// </summary>
        [JsonProperty("playcount")]
        public int PlayCount { get; internal set; }

        /// <summary>
        ///     Main accuracy of the player
        /// </summary>
        [JsonProperty("accuracy")]
        public double Accuracy { get; internal set; }

        /// <summary>
        ///     Global rank of the player
        /// </summary>
        [JsonProperty("pp_rank")]
        public int GlobalRank { get; internal set; }

        /// <summary>
        ///     PP amount of the player
        /// </summary>
        [JsonProperty("pp_raw")]
        public float Pp { get; internal set; }

        /// <summary>
        ///     Country of the account
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; internal set; }

        /// <summary>
        ///     Local rank of the player
        /// </summary>
        [JsonProperty("pp_country_rank")]
        public int RegionalRank { get; internal set; }

        /// <summary>
        ///     Level of the player
        /// </summary>
        [JsonProperty("level")]
        public float Level { get; internal set; }

        /// <summary>
        ///     Total score submitted for this player
        /// </summary>
        [JsonProperty("total_score")]
        public long TotalScore { get; internal set; }

        /// <summary>
        ///     Total ranked score submitted for this player
        /// </summary>
        [JsonProperty("ranked_score")]
        public long RankedScore { get; internal set; }

        /// <summary>
        ///     Count of 300
        /// </summary>
        [JsonProperty("count300")]
        public int Count300 { get; internal set; }

        /// <summary>
        ///     Count of 100
        /// </summary>
        [JsonProperty("count100")]
        public int Count100 { get; internal set; }

        /// <summary>
        ///     Count of 50
        /// </summary>
        [JsonProperty("count50")]
        public int Count50 { get; internal set; }

        /// <summary>
        ///     Count of SS ranks
        /// </summary>
        [JsonProperty("count_rank_ss")]
        public int SsRank { get; internal set; }

        /// <summary>
        ///     Count of S ranks
        /// </summary>
        [JsonProperty("count_rank_s")]
        public int SRank { get; internal set; }

        /// <summary>
        ///     Count of A ranks
        /// </summary>
        [JsonProperty("count_rank_a")]
        public int ARank { get; internal set; }

        [JsonProperty("events")]
        internal List<Event> _events;

        /// <summary>
        ///     Events list of the player
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<Event> Events
            => _events.AsReadOnly();
        
        /// <summary>
        ///     GameMode that user's informations belong to.
        /// </summary>
        [JsonIgnore]
        public GameMode GameMode { get; internal set; }

        internal User()
        {

        }
    }
}