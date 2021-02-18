using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public class EventUserModel
    {
        [JsonProperty("username")]
        public string Username { get; internal set; }
        
        [JsonProperty("url")]
        public string Url { get; internal set; }

        internal EventUserModel()
        {
            
        }
    }
}