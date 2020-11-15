using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class UserPage
    {
        [JsonProperty("html")]
        public string Html { get; internal set; }

        [JsonProperty("raw")]
        public string Raw { get; internal set; }
        
        internal UserPage()
        {
            
        }
    }
}