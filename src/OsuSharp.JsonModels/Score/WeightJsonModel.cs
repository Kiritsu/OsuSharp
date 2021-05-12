using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class WeightJsonModel
    {
        [JsonProperty("percentage")]
        public double Percentage { get; internal set; }

        [JsonProperty("pp")]
        public double PerformancePoints { get; internal set; }

        internal WeightJsonModel()
        {
        }
    }
}