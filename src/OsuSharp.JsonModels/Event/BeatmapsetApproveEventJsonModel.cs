using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class BeatmapsetApproveEventJsonModel : EventJsonModel
    {
        [JsonProperty("approval")]
        public string Approval { get; set; }

        [JsonProperty("beatmapset")]
        public EventBeatmapsetModelJsonModel BeatmapsetJson { get; set; }

        [JsonProperty("user")]
        public EventUserModelJsonModel UserJson { get; set; }
    }
}