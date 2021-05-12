using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class KudosuGiverJsonModel
    {
        [JsonProperty("url")]
        public string Url { get; internal set; }

        [JsonProperty("username")]
        public string Username { get; internal set; }

        internal KudosuGiverJsonModel()
        {
        }
    }
}