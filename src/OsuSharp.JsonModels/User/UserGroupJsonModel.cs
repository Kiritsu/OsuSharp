using System.Collections.Generic;
using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public sealed class UserGroupJsonModel
    {
        [JsonProperty("id")]
        public long Id { get; internal set; }

        [JsonProperty("identifier")]
        public string Identifier { get; internal set; }

        [JsonProperty("is_probationary")]
        public bool IsProbationary { get; internal set; }

        [JsonProperty("name")]
        public string Name { get; internal set; }

        [JsonProperty("short_name")]
        public string ShortName { get; internal set; }

        [JsonProperty("description")]
        public string Description { get; internal set; }

        [JsonProperty("colour")]
        public string Colour { get; internal set; }

        [JsonProperty("play_modes")]
        public IReadOnlyCollection<GameMode> PlayModes { get; internal set; }

        internal UserGroupJsonModel()
        {
        }
    }
}