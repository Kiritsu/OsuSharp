using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class UserRankJsonModel
    {
        [JsonProperty("global")]
        public long Global { get; internal set; }

        [JsonProperty("country")]
        public long Country { get; internal set; }

        internal UserRankJsonModel()
        {
        }
    }
}