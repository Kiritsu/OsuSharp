using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class UserRankHistoryJsonModel : JsonModel
{
    [JsonProperty("mode")]
    public string GameMode { get; set; } = null!;

    [JsonProperty("data")] 
    public List<long> Ranks { get; set; } = new();
}