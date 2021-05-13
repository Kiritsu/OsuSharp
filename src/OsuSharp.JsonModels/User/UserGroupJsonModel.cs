using System.Collections.Generic;
using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public class UserGroupJsonModel : JsonModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("identifier")]
        public string Identifier { get; set; }

        [JsonProperty("is_probationary")]
        public bool IsProbationary { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("colour")]
        public string Colour { get; set; }

        [JsonProperty("play_modes")]
        public IReadOnlyCollection<GameMode> PlayModes { get; set; }
    }
}