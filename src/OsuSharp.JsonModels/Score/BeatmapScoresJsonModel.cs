using Newtonsoft.Json;
using System.Collections.Generic;

namespace OsuSharp.JsonModels;

public class BeatmapScoresJsonModel : JsonModel
{
    [JsonProperty("scores")]
    public List<ScoreJsonModel> Scores { get; set; } = new();

    [JsonProperty("user_score")]
    public BeatmapUserScoreJsonModel UserScore { get; set; } = null!;
}