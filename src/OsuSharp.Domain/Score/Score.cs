using System;
using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class Score : IScore
{
    public long Id { get; internal set; }

    public long? BestId { get; internal set; }

    public long UserId { get; internal set; }

    public double Accuracy { get; internal set; }

    public IReadOnlyList<string> Mods { get; internal set; } = null!;

    public long TotalScore { get; internal set; }

    public int MaxCombo { get; internal set; }

    public bool Perfect { get; internal set; }
        
    public bool Passed { get; internal set; }

    public IStatistics Statistics { get; internal set; } = null!;

    public double? PerformancePoints { get; internal set; }

    public string Rank { get; internal set; } = null!;

    public DateTimeOffset CreatedAt { get; internal set; }

    public GameMode Mode { get; internal set; }

    public bool? HasReplay { get; internal set; }

    public IBeatmap Beatmap { get; internal set; } = null!;

    public IBeatmapsetCompact Beatmapset { get; internal set; } = null!;

    public long? CountryRank { get; internal set; }

    public long? GlobalRank { get; internal set; }

    public IWeight Weight { get; internal set; } = null!;

    public IUserCompact User { get; internal set; } = null!;

    // todo: object
    public object? Match { get; internal set; }

    public IOsuClient Client { get; internal set; } = null!;

    internal Score()
    {
            
    }
}