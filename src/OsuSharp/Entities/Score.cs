using System;
using Newtonsoft.Json;
using OsuSharp.Enums;

namespace OsuSharp.Entities
{
    public sealed class Score : EntityBase
    {
        /// <summary>
        ///     Id of the beatmap.
        /// </summary>
        [JsonProperty("beatmap_id")]
        public long BeatmapId { get; set; }

        /// <summary>
        ///     Total score of the play.
        /// </summary>
        [JsonProperty("score")]
        public long TotalScore { get; internal set; }

        /// <summary>
        ///     Score id.
        /// </summary>
        [JsonProperty("score_id")]
        public long ScoreId { get; internal set; }

        /// <summary>
        ///     Username of the player.
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; internal set; }

        /// <summary>
        ///     Count of 300.
        /// </summary>
        [JsonProperty("count300")]
        public int Count300 { get; internal set; }

        /// <summary>
        ///     Count of 100.
        /// </summary>
        [JsonProperty("count100")]
        public int Count100 { get; internal set; }

        /// <summary>
        ///     Count of 50.
        /// </summary>
        [JsonProperty("count50")]
        public int Count50 { get; internal set; }

        /// <summary>
        ///     Count of misses.
        /// </summary>
        [JsonProperty("countmiss")]
        public int Miss { get; internal set; }

        /// <summary>
        ///     Accuracy of the play.
        /// </summary>
        [JsonIgnore]
        public double Accuracy
            => ((Count50 * 50) + (Count100 * 100) + (Count300 * 300))
                / (300.0 * (Count50 + Count100 + Count300 + Miss)) * 100;

        /// <summary>
        ///     Max combo of the play.
        /// </summary>
        [JsonProperty("maxcombo", NullValueHandling = NullValueHandling.Ignore)]
        public int? MaxCombo { get; internal set; }

        /// <summary>
        ///     Count of katus.
        /// </summary>
        [JsonProperty("countkatu")]
        public int Katu { get; internal set; }

        /// <summary>
        ///     Count of gekies.
        /// </summary>
        [JsonProperty("countgeki")]
        public int Geki { get; internal set; }

        /// <summary>
        ///     Mods enabled for this score.
        /// </summary>
        [JsonProperty("enabled_mods")]
        public Mode Mods { get; internal set; }

        /// <summary>
        ///     Id of the player.
        /// </summary>
        [JsonProperty("user_id")]
        public long Userid { get; internal set; }

        /// <summary>
        ///     Date the score was submitted.
        /// </summary>
        [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset Date { get; internal set; }

        /// <summary>
        ///     Indicates the rank of the map. (SS, SSH, S, SH, A, B, C, D)
        /// </summary>
        [JsonProperty("rank")]
        public string Rank { get; internal set; }

        /// <summary>
        ///     Performance points given by the map.
        /// </summary>
        [JsonProperty("pp")]
        public float PerformancePoints { get; internal set; }

        /// <summary>
        ///     Game mode played for that score.
        /// </summary>
        [JsonIgnore]
        public GameMode? GameMode { get; internal set; } //todo: don't forget to manually assign if possible because it's not provided by the api.
    }
}
