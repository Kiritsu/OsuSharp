using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class WeightJsonModel : JsonModel
{
    [JsonProperty("percentage")]
    public double Percentage { get; set; }

    [JsonProperty("pp")]
    public double PerformancePoints { get; set; }
}