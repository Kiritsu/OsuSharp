using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class AchievementEventJsonModel : EventJsonModel
    {
        [JsonProperty("achievement")]
        public object Achievement { get; internal set; }

        [JsonProperty("user")]
        public EventUserJsonModel UserJson { get; internal set; }

        internal AchievementEventJsonModel()
        {
        }
    }
}