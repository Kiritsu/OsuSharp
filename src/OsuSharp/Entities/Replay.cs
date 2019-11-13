using Newtonsoft.Json;

namespace OsuSharp
{
    public sealed class Replay : EntityBase
    {
        internal Replay() { }

        /// <summary>
        ///     The type of encoding used to encode <see cref="Content"/>. 
        /// </summary>
        [JsonProperty("encoding")]
        public string Encoding { get; internal set; }

        /// <summary>
        ///     Content of the replay. Use the <see cref="ReplayFile"/> to play with the replay or create a osr file from it.
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; internal set; }
    }
}
