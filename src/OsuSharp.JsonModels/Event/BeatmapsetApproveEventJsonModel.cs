using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class BeatmapsetApproveEventJsonModel : EventJsonModel
{
    [JsonProperty("approval")]
    public string Approval { get; set; } = null!;

    [JsonProperty("beatmapset")]
    public EventBeatmapsetModelJsonModel Beatmapset { get; set; } = null!;

    [JsonProperty("user")]
    public EventUserModelJsonModel User { get; set; } = null!;
}