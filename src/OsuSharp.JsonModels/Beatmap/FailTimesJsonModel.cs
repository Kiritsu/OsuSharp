using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class FailTimesJsonModel : JsonModel
{
    [JsonProperty("exit")] 
    public List<int> Exit { get; set; } = new();

    [JsonProperty("fail")] 
    public List<int> Fail { get; set; } = new();
}