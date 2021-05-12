using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public sealed class RankLostEventJsonModel : EventJsonModel
    {
        [JsonProperty("mode")]
        public GameMode GameMode { get; internal set; }
        
        [JsonProperty("beatmap")]
        public EventBeatmapJsonModel BeatmapJson { get; internal set; }
        
        [JsonProperty("user")]
        public EventUserJsonModel UserJson { get; internal set; }

        internal RankLostEventJsonModel()
        {
            
        }
    }
}