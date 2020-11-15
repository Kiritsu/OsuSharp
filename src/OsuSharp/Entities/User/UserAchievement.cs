using System;
using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class UserAchievement
    {
        [JsonProperty("achieved_at")]
        public DateTimeOffset AchievedAt { get; internal set; }

        [JsonProperty("achievement_id")]
        public long AchievementId { get; internal set; }
        
        internal UserAchievement()
        {
            
        }
    }
}