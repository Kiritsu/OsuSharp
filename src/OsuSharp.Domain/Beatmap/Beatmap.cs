using System;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class Beatmap : BeatmapCompact, IBeatmap
{
    public double Accuracy { get; internal set; }

    public double ApproachRate { get; internal set; }

    public long BeatmapsetId { get; internal set; }

    public double Bpm { get; internal set; }

    public bool Converted { get; internal set; }

    public int CircleCount { get; internal set; }

    public int SliderCount { get; internal set; }

    public int SpinnerCount { get; internal set; }

    public double CircleSize { get; internal set; }

    public DateTimeOffset? DeletedAt { get; internal set; }

    public double Drain { get; internal set; }

    public TimeSpan HitLength => _hitLength ??= TimeSpan.FromSeconds(HitLengthSeconds);
    private TimeSpan? _hitLength;
    public int HitLengthSeconds { get; internal set; }

    public bool IsScoreable { get; internal set; }

    public DateTimeOffset LastUpdated { get; internal set; }

    public int PassCount { get; internal set; }

    public int PlayCount { get; internal set; }

    public RankStatus Ranked { get; internal set; }

    public string Url { get; internal set; } = null!;

    public GameMode GameMode { get; internal set; }

    internal Beatmap()
    {
            
    }
}