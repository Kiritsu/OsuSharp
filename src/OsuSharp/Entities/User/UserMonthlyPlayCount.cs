using System;
using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class UserMonthlyPlayCount
    {
        [JsonProperty("start_date")]
        public DateTimeOffset StartDate { get; internal set; }

        [JsonProperty("count")]
        public long Count { get; internal set; }
        
        internal UserMonthlyPlayCount()
        {
            
        }
    }
}