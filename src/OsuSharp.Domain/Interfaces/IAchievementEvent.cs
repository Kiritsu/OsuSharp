namespace OsuSharp.Interfaces;

/// <summary>
/// Defines an achievement event.
/// </summary>
public interface IAchievementEvent : IEvent
{
    /// <summary>
    /// Gets the achievement from the event.
    /// </summary>
    object Achievement { get; }

    /// <summary>
    /// Gets the user that made this achievement.
    /// </summary>
    IEventUserModel User { get; }
}