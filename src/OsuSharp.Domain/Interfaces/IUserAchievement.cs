using System;

namespace OsuSharp.Interfaces;

public interface IUserAchievement
{
    DateTimeOffset AchievedAt { get; }
    long AchievementId { get; }
}