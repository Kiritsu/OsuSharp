using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public sealed class BeatmapsetApproveEventJsonModel : EventJsonModel
    {
        [JsonProperty("approval")]
        public RankStatus Approval { get; internal set; }

        [JsonProperty("beatmapset")]
        public EventBeatmapsetJsonModel BeatmapsetJson { get; internal set; }

        [JsonProperty("user")]
        public EventUserJsonModel UserJson { get; internal set; }

        internal BeatmapsetApproveEventJsonModel()
        {
        }
    }
}