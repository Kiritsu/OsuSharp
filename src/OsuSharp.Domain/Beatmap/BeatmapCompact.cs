using System;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public class BeatmapCompact : IBeatmapCompact
{
    public double DifficultyRating { get; internal set; }

    public long Id { get; internal set; }

    public GameMode Mode { get; internal set; }

    public RankStatus Status { get; internal set; }

    public TimeSpan Length => _length ??= TimeSpan.FromSeconds(TotalLengthSeconds);
    private TimeSpan? _length;
    public long TotalLengthSeconds { get; internal set; }

    public string Version { get; internal set; } = null!;

    public IBeatmapsetCompact Beatmapset { get; internal set; } = null!;

    public string Checksum { get; internal set; } = null!;

    public IFailTimes FailTimes { get; internal set; } = null!;

    public int? MaxCombo { get; internal set; }

    public long UserId { get; internal set; }

    public IOsuClient Client { get; internal set; } = null!;

    internal BeatmapCompact()
    {
            
    }
}