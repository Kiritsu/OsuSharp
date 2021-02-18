using Newtonsoft.Json;
using OsuSharp.Enums;

namespace OsuSharp.Entities
{
    public sealed class RankEvent : Event
    {
        [JsonProperty("scoreRank")]
        public string ScoreRank { get; internal set; }
        
        [JsonProperty("rank")]
        public long Rank { get; internal set; }
        
        [JsonProperty("mode")]
        public GameMode GameMode { get; internal set; }
        
        [JsonProperty("beatmap")]
        public EventBeatmapModel Beatmap { get; internal set; }
        
        [JsonProperty("user")]
        public EventUserModel User { get; internal set; }

        internal RankEvent()
        {
            
        }
    }
}