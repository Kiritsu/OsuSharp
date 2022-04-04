using System;
using System.IO;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

/// <summary>
/// Defines a replay object.
/// </summary>
public interface IReplay
{
    double AdditionalMods { get; }
    short Amount100 { get; }
    short Amount300 { get; }
    short Amount50 { get; }
    short AmountGeki { get; }
    short AmountKatu { get; }
    short AmountMiss { get; }
    string? BeatmapHash { get; }
    ReadOnlyMemory<byte> CompressedReplayData { get; }
    GameMode GameMode { get; }
    string? LifebarGraph { get; }
    short MaxCombo { get; }
    Mods Mods { get; }
    int OsuVersion { get; }
    bool Perfect { get; }
    string? PlayerName { get; }
    string? ReplayHash { get; }
    int ReplayLength { get; }
    long ScoreId { get; }
    DateTime Timestamp { get; }
    int TotalScore { get; }

    void CopyTo(Stream stream);
}