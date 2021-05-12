using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class EventUserJsonModel
    {
        [JsonProperty("username")]
        public string Username { get; internal set; }

        [JsonProperty("url")]
        public string Url { get; internal set; }

        internal EventUserJsonModel()
        {
        }
    }
}