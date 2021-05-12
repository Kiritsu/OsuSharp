using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class EventBeatmapJsonModel
    {
        [JsonProperty("title")]
        public string Title { get; internal set; }
        
        [JsonProperty("url")]
        public string Url { get; internal set; }
        
        internal EventBeatmapJsonModel()
        {
            
        }
    }
}