#pragma warning disable 0649
using System;
using Newtonsoft.Json;

namespace OsuSharp.Endpoints
{
    public class ScoreMatch
    {
        [JsonProperty("pass")]
        private int PassInt { get; set; }

        [JsonProperty("perfect")]
        private int PerfectInt { get; set; }

        /// <summary>
        ///     Slot of the player
        /// </summary>
        [JsonProperty("slot")]
        public int SlotId { get; set; }

        /// <summary>
        ///     Id of the team where the player is playing
        /// </summary>
        [JsonProperty("team")]
        public int TeamId { get; set; }

        /// <summary>
        ///     Id of the player
        /// </summary>
        [JsonProperty("user_id")]
        public long Userid { get; set; }

        /// <summary>
        ///     Score of the player
        /// </summary>
        [JsonProperty("score")]
        public long ScorePoints { get; set; }

        /// <summary>
        ///     Max combo of the player
        /// </summary>
        [JsonProperty("maxcombo")]
        public int? MaxCombo { get; set; }

        /// <summary>
        ///     Rank of the player in the match
        /// </summary>
        [JsonProperty("rank")]
        public int Rank { get; set; }

        /// <summary>
        ///     Count of 50
        /// </summary>
        [JsonProperty("count50")]
        public int Count50 { get; set; }

        /// <summary>
        ///     Count of 100
        /// </summary>
        [JsonProperty("count100")]
        public int Count100 { get; set; }

        /// <summary>
        ///     Count of 300
        /// </summary>
        [JsonProperty("count300")]
        public int Count300 { get; set; }

        /// <summary>
        ///     Count of misses
        /// </summary>
        [JsonProperty("countmiss")]
        public int Miss { get; set; }

        /// <summary>
        ///     Count of gekies
        /// </summary>
        [JsonProperty("countgeki")]
        public int Geki { get; set; }

        /// <summary>
        ///     Count of katus
        /// </summary>
        [JsonProperty("countkatu")]
        public int Katu { get; set; }

        /// <summary>
        ///     Is the play a perfect?
        /// </summary>
        public bool Perfect
            => Convert.ToBoolean(PerfectInt);

        /// <summary>
        ///     Has the player passed the map?
        /// </summary>
        public bool Pass
            => Convert.ToBoolean(PassInt);

        /// <summary>
        ///     Accuracy of the play
        /// </summary>
        [JsonIgnore]
        public double Accuracy => (Count50 * 50 + Count100 * 100 + Count300 * 300)
                                  / (300.0 * (Count50 + Count100 + Count300 + Miss)) * 100;
    }
}