using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class SeasonalBackgroundJsonModel : JsonModel
    {
        [JsonProperty("url")]
        public string Url { get; set;  }

        [JsonProperty("user")]
        public UserCompactJsonModel User { get; set; }
    }
}
