using Newtonsoft.Json;
using OsuSharp.Enums;

namespace OsuSharp.Entities
{
    public sealed class BeatmapsetApproveEvent : Event
    {
        [JsonProperty("approval")]
        public RankStatus Approval { get; internal set; }
        
        [JsonProperty("beatmapset")]
        public EventBeatmapsetModel Beatmapset { get; internal set; }
        
        [JsonProperty("user")]
        public EventUserModel User { get; internal set; }

        internal BeatmapsetApproveEvent()
        {
            
        }
    }
}