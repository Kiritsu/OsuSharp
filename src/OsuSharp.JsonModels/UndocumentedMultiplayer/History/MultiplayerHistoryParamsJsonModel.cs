using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class MultiplayerHistoryParamsJsonModel : JsonModel
{
    [JsonProperty("limit")]
    public int Limit { get; set; }

    [JsonProperty("sort")]
    public string Sort { get; set; }
}