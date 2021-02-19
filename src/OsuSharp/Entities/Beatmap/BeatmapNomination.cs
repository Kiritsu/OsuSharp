using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class BeatmapNomination
    {
        [JsonProperty("current")]
        public int Current { get; internal set; }
        
        [JsonProperty("required")]
        public int Required { get; internal set; }
        
        internal BeatmapNomination()
        {
            
        }
    }
}