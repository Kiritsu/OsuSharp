using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class BeatmapNominationJsonModel
    {
        [JsonProperty("current")]
        public int Current { get; internal set; }
        
        [JsonProperty("required")]
        public int Required { get; internal set; }
        
        internal BeatmapNominationJsonModel()
        {
            
        }
    }
}