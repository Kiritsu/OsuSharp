using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class BeatmapsetReviveEventJsonModel : EventJsonModel
{
    [JsonProperty("beatmapset")]
    public EventBeatmapsetModelJsonModel Beatmapset { get; set; } = null!;

    [JsonProperty("user")]
    public EventUserModelJsonModel User { get; set; } = null!;
}