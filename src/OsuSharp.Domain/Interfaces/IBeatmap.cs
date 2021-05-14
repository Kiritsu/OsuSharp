using System;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces
{
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
        int HitLength { get; }
        bool IsScoreable { get; }
        DateTimeOffset LastUpdated { get; }
        int PassCount { get; }
        int PlayCount { get; }
        RankStatus Ranked { get; }
        string Url { get; }
    }
}