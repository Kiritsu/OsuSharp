using System;
using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class UserCover
    {
        [JsonProperty("custom_url", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string CustomUrl { get; internal set; }

        [JsonProperty("url")]
        public Uri Url { get; internal set; }

        [JsonProperty("id")]
        public string Id { get; internal set; }

        internal UserCover()
        {
            
        }
    }
}