using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class KudosuPostJsonModel : JsonModel
{
    [JsonProperty("url")]
    public string Url { get; set; } = null!;

    [JsonProperty("title")]
    public string Title { get; set; } = null!;
}