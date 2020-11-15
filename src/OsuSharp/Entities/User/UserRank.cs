using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class UserRank
    {
        [JsonProperty("global")]
        public long Global { get; internal set; }

        [JsonProperty("country")]
        public long Country { get; internal set; }
        
        internal UserRank()
        {
            
        }
    }
}