using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class UserStatistics
    {
        [JsonProperty("level")]
        public UserLevel UserLevel { get; internal set; }

        [JsonProperty("pp")]
        public double Pp { get; internal set; }

        [JsonProperty("pp_rank")]
        public long GlobalRank { get; internal set; }

        [JsonProperty("ranked_score")]
        public long RankedScore { get; internal set; }

        [JsonProperty("hit_accuracy")]
        public double HitAccuracy { get; internal set; }

        [JsonProperty("play_count")]
        public long PlayCount { get; internal set; }

        [JsonProperty("play_time")]
        public long PlayTime { get; internal set; }

        [JsonProperty("total_score")]
        public long TotalScore { get; internal set; }

        [JsonProperty("total_hits")]
        public long TotalHits { get; internal set; }

        [JsonProperty("maximum_combo")]
        public long MaximumCombo { get; internal set; }

        [JsonProperty("replays_watched_by_others")]
        public long ReplaysWatchedByOthers { get; internal set; }

        [JsonProperty("is_ranked")]
        public bool IsRanked { get; internal set; }

        [JsonProperty("grade_counts")]
        public UserGradeCounts UserGradeCounts { get; internal set; }

        [JsonProperty("rank")]
        public UserRank UserRank { get; internal set; }
        
        internal UserStatistics()
        {
            
        }
    }
}