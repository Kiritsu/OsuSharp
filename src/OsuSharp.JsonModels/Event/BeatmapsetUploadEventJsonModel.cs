using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class BeatmapsetUploadEventJsonModel : EventJsonModel
    {
        [JsonProperty("beatmapset")]
        public EventBeatmapsetModelJsonModel BeatmapsetJson { get; set; }

        [JsonProperty("user")]
        public EventUserModelJsonModel UserJson { get; set; }
    }
}