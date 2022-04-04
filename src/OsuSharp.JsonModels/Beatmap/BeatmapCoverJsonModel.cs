using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class BeatmapCoverJsonModel : JsonModel
{
    [JsonProperty("cover")]
    public string Cover { get; set; } = null!;

    [JsonProperty("cover@2x")]
    public string Cover2x { get; set; } = null!;

    [JsonProperty("card")]
    public string Card { get; set; } = null!;

    [JsonProperty("card@2x")]
    public string Card2x { get; set; } = null!;

    [JsonProperty("list")]
    public string List { get; set; } = null!;

    [JsonProperty("list@2x")]
    public string List2x { get; set; } = null!;

    [JsonProperty("slimcover")]
    public string SlimCover { get; set; } = null!;

    [JsonProperty("slimcover@2x")]
    public string SlimCover2x { get; set; } = null!;
}