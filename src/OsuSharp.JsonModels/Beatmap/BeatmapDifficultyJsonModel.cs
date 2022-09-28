using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class BeatmapDifficultyJsonModel : JsonModel
{
    [JsonProperty("Attributes")]
    public BeatmapDifficultyAttributesJsonModel Attributes { get; set; } = null!;
}