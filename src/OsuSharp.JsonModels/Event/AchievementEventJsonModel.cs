using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class AchievementEventJsonModel : EventJsonModel
{
    [JsonProperty("achievement")]
    public object Achievement { get; set; } = null!;

    [JsonProperty("user")]
    public EventUserModelJsonModel User { get; set; } = null!;
}