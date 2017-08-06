using Newtonsoft.Json;

namespace OsuSharp.ReplayEndpoint
{
    public class Replay
    {
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("encoding")]
        public string Encoding { get; set; }
    }
}
