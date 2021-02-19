﻿using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class AchievementEvent : Event
    {
        [JsonProperty("achievement")]
        public object Achievement { get; internal set; }

        [JsonProperty("user")]
        public EventUserModel User { get; internal set; }

        internal AchievementEvent()
        {
            
        }
    }
}