using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class KudosuPostJsonModel : JsonModel
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }
    }
}