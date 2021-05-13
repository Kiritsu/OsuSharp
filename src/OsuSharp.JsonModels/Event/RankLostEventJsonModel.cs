using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public class RankLostEventJsonModel : EventJsonModel
    {
        [JsonProperty("mode")]
        public GameMode GameMode { get; set; }

        [JsonProperty("beatmap")]
        public EventBeatmapJsonModel BeatmapJson { get; set; }

        [JsonProperty("user")]
        public EventUserModelJsonModel UserJson { get; set; }
    }
}