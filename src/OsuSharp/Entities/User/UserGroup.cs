using System.Collections.Generic;
using Newtonsoft.Json;
using OsuSharp.Enums;

namespace OsuSharp.Entities
{
    public sealed class UserGroup
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
        
        internal UserGroup()
        {
            
        }
    }
}