using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public class BeatmapsetApproveEventJsonModel : EventJsonModel
    {
        [JsonProperty("approval")]
        public RankStatus Approval { get; set; }

        [JsonProperty("beatmapset")]
        public EventBeatmapsetModelJsonModel BeatmapsetJson { get; set; }

        [JsonProperty("user")]
        public EventUserModelJsonModel UserJson { get; set; }
    }
}