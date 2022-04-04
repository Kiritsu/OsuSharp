using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class BeatmapHypeJsonModel : JsonModel
{
    [JsonProperty("current")]
    public int CurrentHype { get; set; }

    [JsonProperty("required")]
    public int RequiredHype { get; set; }
}