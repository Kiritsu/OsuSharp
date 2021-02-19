using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class FailTimes
    {
        [JsonProperty("exit")]
        public IReadOnlyList<int> Exit { get; internal set; }
        
        [JsonProperty("fail")]
        public IReadOnlyList<int> Fail { get; internal set; }

        internal FailTimes()
        {
            
        }
    }
}