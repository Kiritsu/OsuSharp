using Newtonsoft.Json;

namespace OsuSharp.Entities.Event
{
    public sealed class EventBeatmapModel
    {
        [JsonProperty("title")]
        public string Title { get; internal set; }
        
        [JsonProperty("url")]
        public string Url { get; internal set; }
        
        internal EventBeatmapModel()
        {
            
        }
    }
}