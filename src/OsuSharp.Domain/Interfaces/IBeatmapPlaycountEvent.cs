namespace OsuSharp.Interfaces;

public interface IBeatmapPlaycountEvent : IEvent
{
    int Count { get; }
    IEventBeatmapModel Beatmap { get; }
}