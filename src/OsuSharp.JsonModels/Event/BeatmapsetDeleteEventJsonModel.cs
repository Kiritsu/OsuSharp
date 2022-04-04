using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class BeatmapsetDeleteEventJsonModel : EventJsonModel
{
    [JsonProperty("beatmapset")]
    public EventBeatmapsetModelJsonModel Beatmapset { get; set; } = null!;
}