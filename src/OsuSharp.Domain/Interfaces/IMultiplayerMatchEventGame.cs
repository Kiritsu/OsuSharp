using System;
using System.Collections.Generic;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface IMultiplayerMatchEventGame
{
    long? BeatmapId { get; set; }
    long Id { get; set; }
    DateTimeOffset StartTime { get; set; }
    DateTimeOffset? EndTime { get; set; }
    GameMode Mode { get; set; }
    int ModeInt { get; set; }
    ScoringType ScoringType { get; set; }
    TeamType TeamType { get; set; }
    IReadOnlyList<string> Mods { get; set; }
    IMultiplayerMatchEventGameBeatmap? Beatmap { get; set; }
    IReadOnlyList<IMultiplayerMatchEventGameScore> Scores { get; set; }
}