using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class RankLostEventJsonModel : EventJsonModel
{
    [JsonProperty("mode")]
    public string GameMode { get; set; } = null!;

    [JsonProperty("beatmap")]
    public EventBeatmapJsonModel Beatmap { get; set; } = null!;

    [JsonProperty("user")]
    public EventUserModelJsonModel User { get; set; } = null!;
}