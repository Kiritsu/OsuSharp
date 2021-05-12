using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class BeatmapsetUploadEventJsonModel : EventJsonModel
    {
        [JsonProperty("beatmapset")]
        public EventBeatmapsetJsonModel BeatmapsetJson { get; internal set; }
        
        [JsonProperty("user")]
        public EventUserJsonModel UserJson { get; internal set; }

        internal BeatmapsetUploadEventJsonModel()
        {
            
        }
    }
}