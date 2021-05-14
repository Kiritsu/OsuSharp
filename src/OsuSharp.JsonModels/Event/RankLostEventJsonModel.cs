using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class RankLostEventJsonModel : EventJsonModel
    {
        [JsonProperty("mode")]
        public string GameMode { get; set; }

        [JsonProperty("beatmap")]
        public EventBeatmapJsonModel BeatmapJson { get; set; }

        [JsonProperty("user")]
        public EventUserModelJsonModel UserJson { get; set; }
    }
}