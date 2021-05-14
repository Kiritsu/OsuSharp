using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class UserStatisticsJsonModel : JsonModel
    {
        [JsonProperty("level")]
        public UserLevelJsonModel UserLevelJsonModel { get; set; }

        [JsonProperty("pp")]
        public double Pp { get; set; }

        [JsonProperty("pp_rank")]
        public long GlobalRank { get; set; }

        [JsonProperty("ranked_score")]
        public long RankedScore { get; set; }

        [JsonProperty("hit_accuracy")]
        public double HitAccuracy { get; set; }

        [JsonProperty("play_count")]
        public long PlayCount { get; set; }

        [JsonProperty("play_time")]
        public long PlayTime { get; set; }

        [JsonProperty("total_score")]
        public long TotalScore { get; set; }

        [JsonProperty("total_hits")]
        public long TotalHits { get; set; }

        [JsonProperty("maximum_combo")]
        public long MaximumCombo { get; set; }

        [JsonProperty("replays_watched_by_others")]
        public long ReplaysWatchedByOthers { get; set; }

        [JsonProperty("is_ranked")]
        public bool IsRanked { get; set; }

        [JsonProperty("grade_counts")]
        public UserGradeCountsJsonModel UserGradeCountsJsonModel { get; set; }

        [JsonProperty("rank")]
        public UserRankJsonModel UserRankJsonModel { get; set; }
    }
}