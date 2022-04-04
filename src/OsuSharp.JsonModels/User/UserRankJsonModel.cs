using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class UserRankJsonModel : JsonModel
{
    [JsonProperty("global")]
    public long Global { get; set; }

    [JsonProperty("country")]
    public long Country { get; set; }
}