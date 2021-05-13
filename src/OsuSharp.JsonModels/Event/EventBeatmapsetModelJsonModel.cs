using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class EventBeatmapsetModelJsonModel : JsonModel
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}