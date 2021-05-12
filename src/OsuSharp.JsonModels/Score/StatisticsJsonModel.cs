using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class StatisticsJsonModel
    {
        [JsonProperty("count_50")]
        public int Count50 { get; internal set; }

        [JsonProperty("count_100")]
        public int Count100 { get; internal set; }

        [JsonProperty("count_300")]
        public int Count300 { get; internal set; }

        [JsonProperty("count_geki")]
        public int CountGeki { get; internal set; }

        [JsonProperty("count_katu")]
        public int CountKatu { get; internal set; }

        [JsonProperty("count_miss")]
        public int CountMiss { get; internal set; }

        internal StatisticsJsonModel()
        {
        }
    }
}