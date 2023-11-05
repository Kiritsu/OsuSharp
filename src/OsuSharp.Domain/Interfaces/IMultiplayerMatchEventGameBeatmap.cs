using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface IMultiplayerMatchEventGameBeatmap
{
    long BeatmapsetId { get; set; }
    double DifficultyRating { get; set; }
    long Id { get; set; }
    GameMode Mode { get; set; }
    RankStatus Status { get; set; }
    long TotalLength { get; set; }
    long UserId { get; set; }
    string Version { get; set; }
    IMultiplayerMatchEventGameBeatmapset Beatmapset { get; set; }
}