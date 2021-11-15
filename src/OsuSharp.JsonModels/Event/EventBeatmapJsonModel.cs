using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class EventBeatmapJsonModel : JsonModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}