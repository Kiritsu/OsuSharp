using System;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class UserMonthlyPlayCountJsonModel
    {
        [JsonProperty("start_date")]
        public DateTimeOffset StartDate { get; internal set; }

        [JsonProperty("count")]
        public long Count { get; internal set; }
        
        internal UserMonthlyPlayCountJsonModel()
        {
            
        }
    }
}