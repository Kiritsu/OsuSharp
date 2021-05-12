using System;
using System.Collections.Generic;

namespace OsuSharp.Domain
{
    public class UserCompact 
    {
        public Uri AvatarUrl { get; internal set; }
        
        public string CountryCode { get; internal set; }
        
        public string DefaultGroup { get; internal set; }
        
        public long Id { get; internal set; }
        
        public bool IsActive { get; internal set; }
        
        public bool IsBot { get; internal set; }
        
        public bool IsOnline { get; internal set; }
        
        public bool IsSupporter { get; internal set; }
        
        public DateTimeOffset? LastVisit { get; internal set; }
        
        public bool PmFriendsOnly { get; internal set; }
        
        public string ProfileColour { get; internal set; }
        
        public string Username { get; internal set; }

        public IReadOnlyCollection<UserAccountHistory> AccountHistory { get; internal set; }
        
        public IReadOnlyCollection<UserProfileBanner> TournamentBanner { get; internal set; }
        
        public IReadOnlyCollection<UserBadge> Badges { get; internal set; }
        
        public long? BeatmapPlaycountsCount { get; internal set; }
        
        // todo: type
        public object Blocks { get; internal set; }
        
        public UserCountry Country { get; internal set; }
        
        public UserCover Cover { get; internal set; }
        
        // todo: type
        public object CurrentModeRank { get; internal set; }
        
        public long? FavouriteBeatmapsetCount { get; internal set; }
        
        public long? GraveyardBeatmapsetCount { get; internal set; }
        
        public long? FollowerCount { get; internal set; }
        
        // todo: type
        public object Friends { get; internal set; }
        
        public IReadOnlyCollection<UserGroup> Groups { get; internal set; }

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
        
        public IReadOnlyCollection<UserMonthlyPlayCount> MonthlyPlaycounts { get; internal set; }
        
        public UserPage Page { get; set; }
        
        public IReadOnlyCollection<string> PreviousUsernames { get; internal set; }
        
        public long? RankedAndApprovedBeatmapsetCount { get; internal set; }
        
        public IReadOnlyCollection<UserMonthlyPlayCount> ReplayWatchedCounts { get; internal set; }
        
        public long? ScoresBestCount { get; internal set; }

        public long? ScoresFirstCount { get; internal set; }

        public long? ScoresRecentCount { get; internal set; }
        
        public UserStatistics Statistics { get; internal set; }
        
        public long? SupportLevel { get; internal set; }
        
        public long? UnrankedBeatmapsetCount { get; internal set; }
        
        public long? UnreadPmCount { get; internal set; }
        
        public IReadOnlyCollection<UserAchievement> UserAchievements { get; internal set; }

        public UserRankHistory RankHistory { get; internal set; }
    }
}
