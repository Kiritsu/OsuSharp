using System;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces
{
    public interface IBeatmapCompact
    {
        double DifficultyRating { get; }
        long Id { get; }
        GameMode Mode { get; }
        RankStatus Status { get; }
        TimeSpan Length { get; }
        long TotalLengthSeconds { get; }
        string Version { get; }
        IBeatmapsetCompact Beatmapset { get; }
        string Checksum { get; }
        IFailTimes FailTimes { get; }
        int? MaxCombo { get; }
        long UserId { get; }
    }
}