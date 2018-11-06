using Newtonsoft.Json;

namespace OsuSharp.Endpoints
{
    public sealed class Replay
    {
        /// <summary>
        ///     Replay's content
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; internal set; }

        /// <summary>
        ///     How is the content encoded
        /// </summary>
        [JsonProperty("encoding")]
        public string Encoding { get; internal set; }
    }
}