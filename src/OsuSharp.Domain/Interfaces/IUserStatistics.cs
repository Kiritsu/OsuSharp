namespace OsuSharp.Interfaces
{
    public interface IUserStatistics
    {
        IUserLevel UserLevel { get; }
        double Pp { get; }
        long GlobalRank { get; }
        long RankedScore { get; }
        double HitAccuracy { get; }
        long PlayCount { get; }
        long PlayTime { get; }
        long TotalScore { get; }
        long TotalHits { get; }
        long MaximumCombo { get; }
        long ReplaysWatchedByOthers { get; }
        bool IsRanked { get; }
        IUserGradeCounts UserGradeCounts { get; }
        IUserRank UserRank { get; }
    }
}