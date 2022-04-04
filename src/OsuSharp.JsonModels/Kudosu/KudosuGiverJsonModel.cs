using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class KudosuGiverJsonModel : JsonModel
{
    [JsonProperty("url")]
    public string Url { get; set; } = null!;

    [JsonProperty("username")]
    public string Username { get; set; } = null!;
}