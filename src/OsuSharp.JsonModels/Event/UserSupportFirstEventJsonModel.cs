using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class UserSupportFirstEventJsonModel : EventJsonModel
{
    [JsonProperty("user")]
    public EventUserModelJsonModel User { get; set; } = null!;
}