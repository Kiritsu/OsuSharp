using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class EventBeatmapJsonModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}