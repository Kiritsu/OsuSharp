using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class KudosuPostJsonModel
    {
        [JsonProperty("url")]
        public string Url { get; internal set; }

        [JsonProperty("title")]
        public string Title { get; internal set; }

        internal KudosuPostJsonModel()
        {

        }
    }
}
