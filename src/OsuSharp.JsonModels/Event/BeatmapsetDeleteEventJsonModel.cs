using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class BeatmapsetDeleteEventJsonModel : EventJsonModel
    {
        [JsonProperty("beatmapset")]
        public EventBeatmapsetJsonModel BeatmapsetJson { get; internal set; }

        internal BeatmapsetDeleteEventJsonModel()
        {
            
        }
    }
}