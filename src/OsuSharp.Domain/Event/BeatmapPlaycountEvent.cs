using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class BeatmapPlaycountEvent : Event, IBeatmapPlaycountEvent
{
    public int Count { get; internal set; }

    public IEventBeatmapModel Beatmap { get; internal set; } = null!;

    internal BeatmapPlaycountEvent()
    {
            
    }
}