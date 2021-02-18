using Newtonsoft.Json;
using OsuSharp.Enums;

namespace OsuSharp.Entities
{
    public sealed class RankLostEvent : Event
    {
        [JsonProperty("mode")]
        public GameMode GameMode { get; internal set; }
        
        [JsonProperty("beatmap")]
        public EventBeatmapModel Beatmap { get; internal set; }
        
        [JsonProperty("user")]
        public EventUserModel User { get; internal set; }

        internal RankLostEvent()
        {
            
        }
    }
}