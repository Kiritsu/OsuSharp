using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class BeatmapCompactJsonModel : JsonModel
    {
        [JsonProperty("difficulty_rating")]
        public double DifficultyRating { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }
        
        [JsonProperty("total_length")]
        public long TotalLengthSeconds { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("beatmapset")]
        public BeatmapsetJsonModel Beatmapset { get; set; }

        [JsonProperty("checksum")]
        public string Checksum { get; set; }

        [JsonProperty("failtimes")]
        public FailTimesJsonModel FailTimes { get; set; }

        [JsonProperty("max_combo")]
        public int? MaxCombo { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }
    }
}