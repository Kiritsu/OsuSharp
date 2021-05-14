namespace OsuSharp.Interfaces
{
    public interface IAchievementEvent : IEvent
    {
        object Achievement { get; }
        IEventUserModel User { get; }
    }
}