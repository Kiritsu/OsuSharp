using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class KudosuPost
    {
        [JsonProperty("url")]
        public Optional<string> Url { get; internal set; }

        [JsonProperty("title")]
        public string Title { get; internal set; }

        internal KudosuPost()
        {

        }
    }
}
