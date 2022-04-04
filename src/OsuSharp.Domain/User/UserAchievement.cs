using System;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class UserAchievement : IUserAchievement
{
    public DateTimeOffset AchievedAt { get; internal set; }

    public long AchievementId { get; internal set; }

    internal UserAchievement()
    {
            
    }
}