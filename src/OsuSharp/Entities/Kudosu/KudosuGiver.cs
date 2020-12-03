using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class KudosuGiver
    {
        [JsonProperty("url")]
        public string Url { get; internal set; }

        [JsonProperty("username")]
        public string Username { get; internal set; }

        internal KudosuGiver()
        {

        }
    }
}
