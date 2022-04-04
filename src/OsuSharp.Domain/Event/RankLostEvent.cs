using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class RankLostEvent : Event, IRankLostEvent
{
    public GameMode GameMode { get; internal set; }

    public IEventBeatmapModel Beatmap { get; internal set; } = null!;

    public IEventUserModel User { get; internal set; } = null!;

    internal RankLostEvent()
    {
            
    }
}