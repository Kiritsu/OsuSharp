using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class EventBeatmapJsonModel : JsonModel
{
    [JsonProperty("title")]
    public string Title { get; set; } = null!;

    [JsonProperty("url")]
    public string Url { get; set; } = null!;
}