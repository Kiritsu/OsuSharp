using System.Collections.Generic;
using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public sealed class UserRankHistoryJsonModel
    {
        [JsonProperty("mode")]
        public GameMode GameMode { get; internal set; }

        [JsonProperty("data")]
        public IReadOnlyCollection<long> Ranks { get; internal set; }
        
        internal UserRankHistoryJsonModel()
        {
            
        }
    }
}