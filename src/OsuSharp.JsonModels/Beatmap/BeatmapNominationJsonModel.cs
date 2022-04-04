using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class BeatmapNominationJsonModel : JsonModel
{
    [JsonProperty("current")]
    public int Current { get; set; }

    [JsonProperty("required")]
    public int Required { get; set; }
}