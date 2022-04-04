using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class RankEventJsonModel : EventJsonModel
{
    [JsonProperty("scoreRank")]
    public string ScoreRank { get; set; } = null!;

    [JsonProperty("rank")]
    public long Rank { get; set; }

    [JsonProperty("mode")]
    public string GameMode { get; set; } = null!;

    [JsonProperty("beatmap")]
    public EventBeatmapJsonModel Beatmap { get; set; } = null!;

    [JsonProperty("user")]
    public EventUserModelJsonModel User { get; set; } = null!;
}