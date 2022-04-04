using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class EventUsernameChangeModelJsonModel : EventUserModelJsonModel
{
    [JsonProperty("previous_username")]
    public string PreviousUsername { get; set; } = null!;
}