using System;
using System.Collections.Generic;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public class MultiplayerMatchEventGame : IMultiplayerMatchEventGame
{
    public long? BeatmapId { get; set; }
    public long Id { get; set; }
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset? EndTime { get; set; }
    public GameMode Mode { get; set; }
    public int ModeInt { get; set; }
    public ScoringType ScoringType { get; set; }
    public TeamType TeamType { get; set; }
    public IReadOnlyList<string> Mods { get; set; } = null!;
    public IMultiplayerMatchEventGameBeatmap? Beatmap { get; set; }
    public IReadOnlyList<IMultiplayerMatchEventGameScore> Scores { get; set; } = null!;

    internal MultiplayerMatchEventGame()
    {
        
    }
}