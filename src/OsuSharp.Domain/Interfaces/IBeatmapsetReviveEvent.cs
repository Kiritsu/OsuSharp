namespace OsuSharp.Interfaces;

public interface IBeatmapsetReviveEvent : IEvent
{
    IEventBeatmapsetModel Beatmapset { get; }
    IEventUserModel User { get; }
}