using Newtonsoft.Json;

namespace OsuSharp.ReplayEndpoint
{
    public class Replay
    {
        /// <summary>
        /// Replay's content
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// How is the content encoded
        /// </summary>
        [JsonProperty("encoding")]
        public string Encoding { get; set; }
    }
}