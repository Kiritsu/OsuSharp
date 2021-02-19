using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class BeatmapHype
    {
        [JsonProperty("current")]
        public int CurrentHype { get; internal set; }
        
        [JsonProperty("required")]
        public int RequiredHype { get; internal set; }

        internal BeatmapHype()
        {
            
        }
    }
}