using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface IMultiplayerMatchEventGameBeatmap
{
    long BeatmapsetId { get; }
    double DifficultyRating { get; }
    long Id { get; }
    GameMode Mode { get; }
    RankStatus Status { get; }
    long TotalLength { get; }
    long UserId { get; }
    string Version { get; }
    IMultiplayerMatchEventGameBeatmapset Beatmapset { get; }
}