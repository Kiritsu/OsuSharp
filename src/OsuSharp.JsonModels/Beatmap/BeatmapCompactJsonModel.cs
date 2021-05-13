using System;
using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public class BeatmapCompactJsonModel : JsonModel
    {
        [JsonProperty("difficulty_rating")]
        public double DifficultyRating { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("mode")]
        public GameMode Mode { get; set; }

        [JsonProperty("status")]
        public RankStatus Status { get; set; }

        public TimeSpan Length => _length ??= TimeSpan.FromSeconds(_totalLength);

        [JsonProperty("total_length")]
        private long _totalLength;

        private TimeSpan? _length;

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("beatmapset")]
        public BeatmapsetCompactJsonModel Beatmapset { get; set; }

        [JsonProperty("checksum")]
        public string Checksum { get; set; }

        [JsonProperty("failtimes")]
        public FailTimesJsonModel FailTimesJsonModel { get; set; }

        [JsonProperty("max_combo")]
        public int? MaxCombo { get; set; }
    }
}