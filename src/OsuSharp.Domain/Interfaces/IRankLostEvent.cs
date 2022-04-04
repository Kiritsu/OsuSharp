using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface IRankLostEvent : IEvent
{
    GameMode GameMode { get; }
    IEventBeatmapModel Beatmap { get; }
    IEventUserModel User { get; }
}