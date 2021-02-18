using System;
using Newtonsoft.Json;
using OsuSharp.Enums;

namespace OsuSharp.Entities.Event
{
    public abstract class Event
    {
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; internal set; }
        
        [JsonProperty("id")]
        public long Id { get; internal set; }
        
        [JsonProperty("type")]
        public EventType Type { get; internal set; }
    }
}