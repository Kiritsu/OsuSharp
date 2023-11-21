using System;
using System.Collections.Generic;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface IMultiplayerMatchEventGame
{
    long? BeatmapId { get; }
    long Id { get; }
    DateTimeOffset StartTime { get; }
    DateTimeOffset? EndTime { get; }
    GameMode Mode { get; }
    int ModeInt { get; }
    ScoringType ScoringType { get; }
    TeamType TeamType { get; }
    IReadOnlyList<string> Mods { get; }
    IMultiplayerMatchEventGameBeatmap? Beatmap { get; }
    IReadOnlyList<IMultiplayerMatchEventGameScore> Scores { get; }
}