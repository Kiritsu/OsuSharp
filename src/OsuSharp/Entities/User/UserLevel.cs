using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class UserLevel
    {
        [JsonProperty("current")]
        public long Current { get; internal set; }

        [JsonProperty("progress")]
        public long Progress { get; internal set; }
        
        internal UserLevel()
        {
            
        }
    }
}