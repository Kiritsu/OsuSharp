using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class UserPageJsonModel
    {
        [JsonProperty("html")]
        public string Html { get; internal set; }

        [JsonProperty("raw")]
        public string Raw { get; internal set; }

        internal UserPageJsonModel()
        {
        }
    }
}