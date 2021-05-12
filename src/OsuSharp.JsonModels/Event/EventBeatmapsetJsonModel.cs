using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class EventBeatmapsetJsonModel
    {
        [JsonProperty("title")]
        public string Title { get; internal set; }
        
        [JsonProperty("url")]
        public string Url { get; internal set; }
        
        internal EventBeatmapsetJsonModel()
        {
            
        }
    }
}