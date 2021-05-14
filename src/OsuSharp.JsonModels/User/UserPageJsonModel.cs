using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class UserPageJsonModel : JsonModel
    {
        [JsonProperty("html")]
        public string Html { get; set; }

        [JsonProperty("raw")]
        public string Raw { get; set; }
    }
}