using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class UserKudosuJsonModel : JsonModel
{
    [JsonProperty("total")]
    public long Total { get; set; }

    [JsonProperty("available")]
    public long Available { get; set; }
}