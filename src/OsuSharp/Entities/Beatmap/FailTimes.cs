using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class FailTimes
    {
        [JsonProperty("exit")]
        public Optional<IReadOnlyList<int>> Exit { get; internal set; }
        
        [JsonProperty("fail")]
        public Optional<IReadOnlyList<int>> Fail { get; internal set; }

        internal FailTimes()
        {
            
        }
    }
}