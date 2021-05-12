using System;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class UserAccountHistoryJsonModel
    {
        [JsonProperty("id")]
        public long Id { get; internal set; }

        [JsonProperty("type")]
        public string Type { get; internal set; }

        [JsonProperty("timestamp")]
        public DateTimeOffset TimeStamp { get; internal set; }

        [JsonProperty("length")]
        public int Length { get; internal set; }

        internal UserAccountHistoryJsonModel()
        {
        }
    }
}