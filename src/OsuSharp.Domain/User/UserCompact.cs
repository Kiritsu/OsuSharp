using OsuSharp.Interfaces;
using System.Collections.Generic;

namespace OsuSharp.Domain
{
    public class UserCompact : UserCompactBase, IUserCompact
    {
        public IReadOnlyList<IUserAccountHistory> AccountHistory { get; internal set; }
        public IReadOnlyList<IUserProfileBanner> TournamentBanner { get; internal set; }
        public IReadOnlyList<IUserBadge> Badges { get; internal set; }
        public long? BeatmapPlaycountsCount { get; internal set; }
        public object Blocks { get; internal set; }
        public IUserCountry Country { get; internal set; }
        public IUserCover Cover { get; internal set; }
        public long? FavouriteBeatmapsetCount { get; internal set; }
        public long? GraveyardBeatmapsetCount { get; internal set; }
        public long? FollowerCount { get; internal set; }
        public object Friends { get; internal set; }
        public IReadOnlyList<IUserGroup> Groups { get; internal set; }
        public bool? IsAdmin { get; internal set; }
        public bool? IsBng { get; internal set; }
        public bool? IsFullBng { get; internal set; }
        public bool? IsGmt { get; internal set; }
        public bool? IsLimitedBn { get; internal set; }
        public bool? IsModerator { get; internal set; }
        public bool? IsNat { get; internal set; }
        public bool? IsRestricted { get; internal set; }
        public bool? IsSilenced { get; internal set; }
        public long? LovedBeatmapsetCount { get; internal set; }
        public IReadOnlyList<IUserMonthlyPlayCount> MonthlyPlaycounts { get; internal set; }
        public IUserPage Page { get; set; }
        public IReadOnlyList<string> PreviousUsernames { get; internal set; }
        public long? RankedBeatmapsetCount { get; internal set; }
        public IReadOnlyList<IUserMonthlyPlayCount> ReplayWatchedCounts { get; internal set; }
        public long? ScoresBestCount { get; internal set; }
        public long? ScoresFirstCount { get; internal set; }
        public long? ScoresRecentCount { get; internal set; }
        public IUserStatistics Statistics { get; internal set; }
        public long? SupportLevel { get; internal set; }
        public long? PendingBeatmapsetCount { get; internal set; }
        public long? UnreadPmCount { get; internal set; }
        public IReadOnlyList<IUserAchievement> UserAchievements { get; internal set; }
        public IUserRankHistory RankHistory { get; internal set; }
        public long? CommentsCount { get; set; }
        public bool? IsDeleted { get; set; }
        public List<string> ProfileOrder { get; set; }
        public string TitleUrl { get; set; }
        public long? MappingFollowerCount { get; set; }
        public object ReplaysWatchedCounts { get; set; }

        internal UserCompact()
        {
            
        }
    }
}