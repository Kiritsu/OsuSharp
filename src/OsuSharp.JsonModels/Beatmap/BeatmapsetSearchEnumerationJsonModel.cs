using Newtonsoft.Json;
using System.Collections.Generic;

namespace OsuSharp.JsonModels;

public class BeatmapsetSearchEnumerationJsonModel : JsonModel
{
    [JsonProperty("beatmapsets")] 
    public List<BeatmapsetJsonModel> Beatmapsets { get; set; } = new();

    [JsonProperty("cursor")]
    public CursorJsonModel Cursor { get; set; } = null!;

    [JsonProperty("search")]
    public SearchJsonModel Search { get; set; } = null!;

    [JsonProperty("recommended_difficulty")]
    public double? RecommendedDifficulty { get; set; }

    [JsonProperty("error")]
    public object? Error { get; set; }

    [JsonProperty("total")]
    public long Total { get; set; }

    [JsonProperty("cursor_string")] 
    public string CursorString { get; set; } = null!;
}