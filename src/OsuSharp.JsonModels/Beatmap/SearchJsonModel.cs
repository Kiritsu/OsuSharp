using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class SearchJsonModel : JsonModel
{
    [JsonProperty("sort")]
    public string Sort { get; set; } = null!;
}