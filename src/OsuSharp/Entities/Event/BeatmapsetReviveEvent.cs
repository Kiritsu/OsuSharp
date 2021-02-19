﻿using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class BeatmapsetReviveEvent : Event
    {
        [JsonProperty("beatmapset")]
        public EventBeatmapsetModel Beatmapset { get; internal set; }
        
        [JsonProperty("user")]
        public EventUserModel User { get; internal set; }

        internal BeatmapsetReviveEvent()
        {
            
        }
    }
}