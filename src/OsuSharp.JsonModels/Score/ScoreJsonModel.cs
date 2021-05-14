using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class ScoreJsonModel : JsonModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("best_id")]
        public long? BestId { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("accuracy")]
        public double Accuracy { get; set; }

        [JsonProperty("mods")]
        public IReadOnlyList<string> Mods { get; set; }

        [JsonProperty("score")]
        public long TotalScore { get; set; }

        [JsonProperty("max_combo")]
        public int MaxCombo { get; set; }

        [JsonProperty("perfect")]
        public bool Perfect { get; set; }

        [JsonProperty("statistics")]
        public StatisticsJsonModel Statistics { get; set; }

        [JsonProperty("pp")]
        public double? PerformancePoints { get; set; }

        [JsonProperty("rank")]
        public string Rank { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("mode")]
        public string Mode { get; set; }

        [JsonProperty("replay")]
        public bool? HasReplay { get; set; }

        [JsonProperty("beatmap")]
        public BeatmapJsonModel Beatmap { get; set; }

        [JsonProperty("beatmapset")]
        public BeatmapsetJsonModel Beatmapset { get; set; }

        [JsonProperty("rank_country")]
        public long? CountryRank { get; set; }

        [JsonProperty("rank_global")]
        public long? GlobalRank { get; set; }

        [JsonProperty("weight")]
        public WeightJsonModel Weight { get; set; }

        [JsonProperty("user")]
        public UserJsonModel User { get; set; }

        // todo: object
        [JsonProperty("match")]
        public object Match { get; set; }
    }
}