using System;

namespace OsuSharp.Domain
{
    public sealed class UserAchievement
    {
        public DateTimeOffset AchievedAt { get; internal set; }

        public long AchievementId { get; internal set; }

        internal UserAchievement()
        {
            
        }
    }
}