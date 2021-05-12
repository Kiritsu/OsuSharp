using System;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class UserAchievementJsonModel
    {
        [JsonProperty("achieved_at")]
        public DateTimeOffset AchievedAt { get; internal set; }

        [JsonProperty("achievement_id")]
        public long AchievementId { get; internal set; }
        
        internal UserAchievementJsonModel()
        {
            
        }
    }
}