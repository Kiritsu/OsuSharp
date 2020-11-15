using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class UserKudosu
    {
        [JsonProperty("total")]
        public long Total { get; internal set; }

        [JsonProperty("available")]
        public long Available { get; internal set; }

        internal UserKudosu()
        {
            
        }
    }
}