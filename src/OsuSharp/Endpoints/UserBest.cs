#pragma warning disable 0649
using System;
using Newtonsoft.Json;
using OsuSharp.Enums;
using OsuSharp.Misc;

namespace OsuSharp.Endpoints
{
    public sealed class UserBest : Endpoint
    {
        [JsonProperty("perfect")]
        internal int _perfect;

        /// <summary>
        ///     Id of the beatmap
        /// </summary>
        [JsonProperty("beatmap_id")]
        public long BeatmapId { get; set; }

        /// <summary>
        ///     Score points of the play
        /// </summary>
        [JsonProperty("score")]
        public long TotalScore { get; set; }

        /// <summary>
        ///     Name of the player
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

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
        ///     Count of misses
        /// </summary>
        [JsonProperty("countmiss")]
        public int Miss { get; set; }

        /// <summary>
        ///     Accuracy of the play
        /// </summary>
        [JsonIgnore]
        public double Accuracy 
            => ((Count50 * 50) + (Count100 * 100) + (Count300 * 300))
                / (300.0 * (Count50 + Count100 + Count300 + Miss)) * 100;

        /// <summary>
        ///     Max combo of the play
        /// </summary>
        [JsonProperty("maxcombo", NullValueHandling = NullValueHandling.Ignore)]
        public int MaxCombo { get; set; }

        /// <summary>
        ///     Count of katus
        /// </summary>
        [JsonProperty("countkatu")]
        public int Katu { get; set; }

        /// <summary>
        ///     Count of gekies
        /// </summary>
        [JsonProperty("countgeki")]
        public int Geki { get; set; }

        /// <summary>
        ///     Is the map a perfect?
        /// </summary>
        [JsonIgnore]
        public bool Perfect
            => Convert.ToBoolean(_perfect);

        /// <summary>
        ///     Mods used for this play
        /// </summary>
        [JsonProperty("enabled_mods")]
        public int EnabledMods { get; set; }

        /// <summary>
        ///     Better representation of 'EnabledMods'
        /// </summary>
        [JsonIgnore]
        public Mods Mods
            => (Mods)EnabledMods;

        /// <summary>
        ///     Id of the user
        /// </summary>
        [JsonProperty("user_id")]
        public long Userid { get; set; }

        /// <summary>
        ///     Date the play was submitted
        /// </summary>
        [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Date { get; set; }

        /// <summary>
        ///     Rank of the player in this play
        /// </summary>
        [JsonProperty("rank")]
        public string Rank { get; set; }

        /// <summary>
        ///     Pp given by the play
        /// </summary>
        [JsonProperty("pp")]
        public float Pp { get; set; }

        /// <summary>
        ///     GameMode that scores's informations belong to.
        /// </summary>
        [JsonIgnore]
        public GameMode GameMode { get; internal set; }

        internal UserBest()
        {

        }
    }
}