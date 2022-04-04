using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class UserScoresJsonModel : JsonModel
{
    [JsonProperty("scores")] 
    public List<ScoreJsonModel> Scores { get; set; } = new();
}