using System;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

/// <summary>
/// Defines a beatmap object.
/// </summary>
public interface IBeatmap : IBeatmapCompact
{
    double Accuracy { get; }
    double ApproachRate { get; }
    long BeatmapsetId { get; }
    double Bpm { get; }
    bool Converted { get; }
    int CircleCount { get; }
    int SliderCount { get; }
    int SpinnerCount { get; }
    double CircleSize { get; }
    DateTimeOffset? DeletedAt { get; }
    double Drain { get; }
    TimeSpan HitLength { get; }
    int HitLengthSeconds { get; }
    bool IsScoreable { get; }
    DateTimeOffset LastUpdated { get; }
    int PassCount { get; }
    int PlayCount { get; }
    RankStatus Ranked { get; }
    string Url { get; }
    GameMode GameMode { get; }
}