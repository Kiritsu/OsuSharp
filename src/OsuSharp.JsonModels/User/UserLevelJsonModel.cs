using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class UserLevelJsonModel : JsonModel
{
    [JsonProperty("current")]
    public long Current { get; set; }

    [JsonProperty("progress")]
    public long Progress { get; set; }
}