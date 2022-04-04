using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class AchievementEvent : Event, IAchievementEvent
{
    public object Achievement { get; internal set; } = null!;

    public IEventUserModel User { get; internal set; } = null!;

    internal AchievementEvent()
    {
            
    }
}