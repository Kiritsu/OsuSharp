using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class BeatmapCoverJsonModel
    {
        [JsonProperty("cover")]
        public string Cover { get; internal set; }
        
        [JsonProperty("cover@2x")]
        public string Cover2x { get; internal set; }
        
        [JsonProperty("card")]
        public string Card { get; internal set; }
        
        [JsonProperty("card@2x")]
        public string Card2x { get; internal set; }
        
        [JsonProperty("list")]
        public string List { get; internal set; }
        
        [JsonProperty("list@2x")]
        public string List2x { get; internal set; }
        
        [JsonProperty("slim")]
        public string SlimCover { get; internal set; }
        
        [JsonProperty("slimcover@2x")]
        public string SlimCover2x { get; internal set; }

        internal BeatmapCoverJsonModel()
        {
            
        }
    }
}