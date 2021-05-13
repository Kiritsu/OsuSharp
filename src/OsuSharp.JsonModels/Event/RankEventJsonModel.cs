using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public class RankEventJsonModel : EventJsonModel
    {
        [JsonProperty("scoreRank")]
        public string ScoreRank { get; set; }

        [JsonProperty("rank")]
        public long Rank { get; set; }

        [JsonProperty("mode")]
        public GameMode GameMode { get; set; }

        [JsonProperty("beatmap")]
        public EventBeatmapJsonModel BeatmapJson { get; set; }

        [JsonProperty("user")]
        public EventUserModelJsonModel UserJson { get; set; }
    }
}