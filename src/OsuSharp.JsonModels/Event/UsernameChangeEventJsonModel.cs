using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class UsernameChangeEventJsonModel : EventJsonModel
{
    [JsonProperty("user")]
    public EventUsernameChangeModelJsonModel User { get; set; } = null!;
}