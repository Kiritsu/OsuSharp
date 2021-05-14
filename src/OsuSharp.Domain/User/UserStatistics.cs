using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class UserStatistics : IUserStatistics
    {
        public IUserLevel UserLevel { get; internal set; }

        public double Pp { get; internal set; }

        public long GlobalRank { get; internal set; }

        public long RankedScore { get; internal set; }

        public double HitAccuracy { get; internal set; }

        public long PlayCount { get; internal set; }

        public long PlayTime { get; internal set; }

        public long TotalScore { get; internal set; }

        public long TotalHits { get; internal set; }

        public long MaximumCombo { get; internal set; }

        public long ReplaysWatchedByOthers { get; internal set; }

        public bool IsRanked { get; internal set; }

        public IUserGradeCounts UserGradeCounts { get; internal set; }

        public IUserRank UserRank { get; internal set; }

        internal UserStatistics()
        {
            
        }
    }
}