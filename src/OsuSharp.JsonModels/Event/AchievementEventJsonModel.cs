using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class AchievementEventJsonModel : EventJsonModel
    {
        [JsonProperty("achievement")]
        public object Achievement { get; set; }

        [JsonProperty("user")]
        public EventUserModelJsonModel UserJson { get; set; }
    }
}