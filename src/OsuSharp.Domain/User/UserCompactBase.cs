using System;
using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public class UserCompactBase : IUserCompactBase
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
        public IReadOnlyCollection<IUserAccountHistory> AccountHistory { get; internal set; }
        public IReadOnlyCollection<IUserProfileBanner> TournamentBanner { get; internal set; }
        public IReadOnlyCollection<IUserBadge> Badges { get; internal set; }
        public long? BeatmapPlaycountsCount { get; internal set; }
        public object Blocks { get; internal set; }
        public IUserCountry Country { get; internal set; }
        public IUserCover Cover { get; internal set; }
        public object CurrentModeRank { get; internal set; }
        public long? FavouriteBeatmapsetCount { get; internal set; }
        public long? GraveyardBeatmapsetCount { get; internal set; }
        public long? FollowerCount { get; internal set; }
        public object Friends { get; internal set; }
        public IReadOnlyCollection<IUserGroup> Groups { get; internal set; }
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
        public IReadOnlyCollection<IUserMonthlyPlayCount> MonthlyPlaycounts { get; internal set; }
        public IUserPage Page { get; set; }
        public IReadOnlyCollection<string> PreviousUsernames { get; internal set; }
        public long? RankedAndApprovedBeatmapsetCount { get; internal set; }
        public IReadOnlyCollection<IUserMonthlyPlayCount> ReplayWatchedCounts { get; internal set; }
        public long? ScoresBestCount { get; internal set; }
        public long? ScoresFirstCount { get; internal set; }
        public long? ScoresRecentCount { get; internal set; }
        public IUserStatistics Statistics { get; internal set; }
        public long? SupportLevel { get; internal set; }
        public long? UnrankedBeatmapsetCount { get; internal set; }
        public long? UnreadPmCount { get; internal set; }
        public IReadOnlyCollection<IUserAchievement> UserAchievements { get; internal set; }
        public IUserRankHistory RankHistory { get; internal set; }
    }
}