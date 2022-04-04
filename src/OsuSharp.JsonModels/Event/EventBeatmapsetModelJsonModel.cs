using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class EventBeatmapsetModelJsonModel : JsonModel
{
    [JsonProperty("title")]
    public string Title { get; set; } = null!;

    [JsonProperty("url")]
    public string Url { get; set; } = null!;
}