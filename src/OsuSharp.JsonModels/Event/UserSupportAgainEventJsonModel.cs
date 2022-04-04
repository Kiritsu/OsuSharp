using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class UserSupportAgainEventJsonModel : EventJsonModel
{
    [JsonProperty("user")]
    public EventUserModelJsonModel User { get; set; } = null!;
}