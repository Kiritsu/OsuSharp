using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface IRankEvent : IEvent
{
    string ScoreRank { get; }
    long Rank { get; }
    GameMode GameMode { get; }
    IEventBeatmapModel Beatmap { get; }
    IEventUserModel User { get; }
}