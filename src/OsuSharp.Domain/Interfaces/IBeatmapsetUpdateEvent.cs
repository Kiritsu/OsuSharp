namespace OsuSharp.Interfaces;

public interface IBeatmapsetUpdateEvent : IEvent
{
    IEventBeatmapsetModel Beatmapset { get; }
    IEventUserModel User { get; }
}