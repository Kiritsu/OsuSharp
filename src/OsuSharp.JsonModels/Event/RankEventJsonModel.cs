using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public sealed class RankEventJsonModel : EventJsonModel
    {
        [JsonProperty("scoreRank")]
        public string ScoreRank { get; internal set; }
        
        [JsonProperty("rank")]
        public long Rank { get; internal set; }
        
        [JsonProperty("mode")]
        public GameMode GameMode { get; internal set; }
        
        [JsonProperty("beatmap")]
        public EventBeatmapJsonModel BeatmapJson { get; internal set; }
        
        [JsonProperty("user")]
        public EventUserJsonModel UserJson { get; internal set; }

        internal RankEventJsonModel()
        {
            
        }
    }
}