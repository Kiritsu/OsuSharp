using Newtonsoft.Json;

namespace OsuSharp.Entities.Event
{
    public sealed class BeatmapsetUploadEvent : Event
    {
        [JsonProperty("beatmapset")]
        public EventBeatmapsetModel Beatmapset { get; internal set; }
        
        [JsonProperty("user")]
        public EventUserModel User { get; internal set; }

        internal BeatmapsetUploadEvent()
        {
            
        }
    }
}