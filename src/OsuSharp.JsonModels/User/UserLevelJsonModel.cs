using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class UserLevelJsonModel
    {
        [JsonProperty("current")]
        public long Current { get; internal set; }

        [JsonProperty("progress")]
        public long Progress { get; internal set; }

        internal UserLevelJsonModel()
        {
        }
    }
}