using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class MultiplayerMatchEventDetailsJsonModel : JsonModel
{
    [JsonProperty("type")]
    public string Type { get; set; } = null!;

    [JsonProperty("text")] 
    public string Text { get; set; } = null!;
}