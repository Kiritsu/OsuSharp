using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class SeasonalBackgroundJsonModel : JsonModel
{
    [JsonProperty("url")]
    public string Url { get; set;  } = null!;

    [JsonProperty("user")]
    public UserJsonModel User { get; set; } = null!;
}