using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class UserKudosuJsonModel
    {
        [JsonProperty("total")]
        public long Total { get; internal set; }

        [JsonProperty("available")]
        public long Available { get; internal set; }

        internal UserKudosuJsonModel()
        {
        }
    }
}