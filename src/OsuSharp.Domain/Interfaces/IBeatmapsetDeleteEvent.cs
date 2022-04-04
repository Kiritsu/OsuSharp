namespace OsuSharp.Interfaces;

public interface IBeatmapsetDeleteEvent : IEvent
{
    IEventBeatmapsetModel Beatmapset { get; }
}