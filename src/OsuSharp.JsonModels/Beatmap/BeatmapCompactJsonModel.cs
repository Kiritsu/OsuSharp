using System;
using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public class BeatmapCompactJsonModel : JsonModel
    {
        [JsonProperty("difficulty_rating")]
        public double DifficultyRating { get; internal set; }

        [JsonProperty("id")]
        public long Id { get; internal set; }

        [JsonProperty("mode")]
        public GameMode Mode { get; internal set; }

        [JsonProperty("status")]
        public RankStatus Status { get; internal set; }

        public TimeSpan Length => _length ??= TimeSpan.FromSeconds(_totalLength);

        [JsonProperty("total_length")]
        private long _totalLength;

        private TimeSpan? _length;

        [JsonProperty("version")]
        public string Version { get; internal set; }

        [JsonProperty("beatmapset")]
        public BeatmapsetCompactJsonModel Beatmapset { get; internal set; }

        [JsonProperty("checksum")]
        public string Checksum { get; internal set; }

        [JsonProperty("failtimes")]
        public FailTimesJsonModel FailTimesJsonModel { get; internal set; }

        [JsonProperty("max_combo")]
        public int? MaxCombo { get; internal set; }

        internal BeatmapCompactJsonModel()
        {
        }
    }
}