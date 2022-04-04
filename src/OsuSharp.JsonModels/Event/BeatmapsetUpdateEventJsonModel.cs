using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class BeatmapsetUpdateEventJsonModel : EventJsonModel
{
    [JsonProperty("beatmapset")]
    public EventBeatmapsetModelJsonModel Beatmapset { get; set; } = null!;

    [JsonProperty("user")]
    public EventUserModelJsonModel User { get; set; } = null!;
}