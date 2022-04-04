using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class EventUserModelJsonModel : JsonModel
{
    [JsonProperty("username")]
    public string Username { get; set; } = null!;

    [JsonProperty("url")]
    public string Url { get; set; } = null!;
}