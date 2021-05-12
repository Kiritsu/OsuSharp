using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class BeatmapPlaycountEventJsonModel : EventJsonModel
    {
        [JsonProperty("count")]
        public int Count { get; internal set; }

        [JsonProperty("beatmap")]
        public EventBeatmapJsonModel BeatmapJson { get; internal set; }

        internal BeatmapPlaycountEventJsonModel()
        {
        }
    }
}