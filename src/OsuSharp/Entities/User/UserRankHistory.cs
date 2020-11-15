using System.Collections.Generic;
using Newtonsoft.Json;
using OsuSharp.Enums;

namespace OsuSharp.Entities
{
    public sealed class UserRankHistory
    {
        [JsonProperty("mode")]
        public GameMode GameMode { get; internal set; }

        [JsonProperty("data")]
        public IReadOnlyCollection<long> Ranks { get; internal set; }
        
        internal UserRankHistory()
        {
            
        }
    }
}