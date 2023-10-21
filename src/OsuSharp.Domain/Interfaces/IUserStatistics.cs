namespace OsuSharp.Interfaces;

public interface IUserStatistics
{
    IUserLevel UserLevel { get; }
    double? Pp { get; }
    long? PpExp { get; }
    long RankedScore { get; }
    double HitAccuracy { get; }
    long PlayCount { get; }
    long? PlayTime { get; }
    long TotalScore { get; }
    long TotalHits { get; }
    long? Count100 { get; }
    long? Count300 { get; }
    long? Count50 { get; }
    long? CountMiss { get; }
    long MaximumCombo { get; }
    long ReplaysWatchedByOthers { get; }
    bool IsRanked { get; }
    IUserGradeCounts UserGradeCounts { get; }
    long? CountryRank { get; }
    long? GlobalRank { get; }
    long? GlobalRankExp { get; }
    IUserCompact? User { get; }
}