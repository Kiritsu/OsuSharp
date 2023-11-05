using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public class MultiplayerMatchEventGameBeatmap : IMultiplayerMatchEventGameBeatmap
{
    public long BeatmapsetId { get; set; }
    public double DifficultyRating { get; set; }
    public long Id { get; set; }
    public GameMode Mode { get; set; }
    public RankStatus Status { get; set; }
    public long TotalLength { get; set; }
    public long UserId { get; set; }
    public string Version { get; set; } = null!;
    public IMultiplayerMatchEventGameBeatmapset Beatmapset { get; set; } = null!;

    internal MultiplayerMatchEventGameBeatmap()
    {
        
    }
}