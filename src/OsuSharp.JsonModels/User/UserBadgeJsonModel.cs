using System;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class UserBadgeJsonModel : JsonModel
    {
        [JsonProperty("awarded_at")]
        public DateTimeOffset AwardedAt { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image_url")]
        public string ImageUrl { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}