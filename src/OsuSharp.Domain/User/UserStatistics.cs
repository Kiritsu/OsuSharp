using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class UserStatistics : IUserStatistics
{
    public IUserLevel UserLevel { get; internal set; } = null!;

    public double? Pp { get; internal set; }
    
    public long? PpExp { get; internal set; }

    public long RankedScore { get; internal set; }

    public double HitAccuracy { get; internal set; }

    public long PlayCount { get; internal set; }

    public long? PlayTime { get; internal set; }

    public long TotalScore { get; internal set; }

    public long TotalHits { get; internal set; }
    
    public long? Count100 { get; internal set; }
    
    public long? Count300 { get; internal set; }
    
    public long? Count50 { get; internal set; }
    
    public long? CountMiss { get; internal set; }

    public long MaximumCombo { get; internal set; }

    public long ReplaysWatchedByOthers { get; internal set; }

    public bool IsRanked { get; internal set; }

    public IUserGradeCounts UserGradeCounts { get; internal set; } = null!;

    public long? CountryRank { get; internal set; }

    public long? GlobalRank { get; internal set; }
    
    public long? GlobalRankExp { get; internal set; }

    public IUserCompact? User { get; internal set; } 

    internal UserStatistics()
    {
    }
}