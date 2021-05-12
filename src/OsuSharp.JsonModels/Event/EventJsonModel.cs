using System;
using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public abstract class EventJsonModel : JsonModel
    {
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; internal set; }

        [JsonProperty("id")]
        public long Id { get; internal set; }

        [JsonProperty("type")]
        public EventType Type { get; internal set; }
    }
}