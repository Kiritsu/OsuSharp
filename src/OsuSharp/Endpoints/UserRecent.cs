#pragma warning disable 0649
using System;
using Newtonsoft.Json;
using OsuSharp.Misc;

namespace OsuSharp.Endpoints
{
    public sealed class UserRecent : Endpoint
    {
        [JsonProperty("perfect")]
        internal int _perfect;

        /// <summary>
        ///     Id of the beatmaps
        /// </summary>
        [JsonProperty("beatmap_id")]
        public long BeatmapId { get; internal set; }

        /// <summary>
        ///     Total score of the player in this play
        /// </summary>
        [JsonProperty("score")]
        public long ScorePoints { get; internal set; }

        /// <summary>
        ///     Max combo of the play
        /// </summary>
        [JsonProperty("maxcombo", NullValueHandling = NullValueHandling.Ignore)]
        public int MaxCombo { get; internal set; }

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
        ///     Accuracy of this play
        /// </summary>
        [JsonIgnore]
        public double Accuracy
            => ((Count50 * 50) + (Count100 * 100) + (Count300 * 300))
                / (300.0 * (Count50 + Count100 + Count300 + Miss)) * 100;

        /// <summary>
        ///     Count of misses
        /// </summary>
        [JsonProperty("countmiss")]
        public int Miss { get; internal set; }

        /// <summary>
        ///     Count of katus
        /// </summary>
        [JsonProperty("countkatu")]
        public int Katu { get; internal set; }

        /// <summary>
        ///     Count of gekies
        /// </summary>
        [JsonProperty("countgeki")]
        public int Geki { get; internal set; }

        /// <summary>
        ///     Is this map a perfect?
        /// </summary>
        [JsonIgnore]
        public bool Perfect
            => Convert.ToBoolean(_perfect);

        /// <summary>
        ///     Mods used for this play
        /// </summary>
        [JsonProperty("enabled_mods")]
        public int EnabledMods { get; internal set; }

        /// <summary>
        ///     Better representation of EnabledMods
        /// </summary>
        [JsonIgnore]
        public Mods Mods
            => (Mods)EnabledMods;

        /// <summary>
        ///     Id of the player
        /// </summary>
        [JsonProperty("user_id")]
        public long Userid { get; internal set; }

        /// <summary>
        ///     Date the score was submitted
        /// </summary>
        [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Date { get; internal set; }

        /// <summary>
        ///     Rank of the user for this play in the leaderboard
        /// </summary>
        [JsonProperty("rank")]
        public string Rank { get; internal set; }

        internal UserRecent()
        {

        }
    }
}