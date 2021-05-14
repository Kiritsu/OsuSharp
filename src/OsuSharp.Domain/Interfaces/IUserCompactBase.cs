using System;
using System.Collections.Generic;

namespace OsuSharp.Interfaces
{
    public interface IUserCompactBase
    {
        Uri AvatarUrl { get; }
        string CountryCode { get; }
        string DefaultGroup { get; }
        long Id { get; }
        bool IsActive { get; }
        bool IsBot { get; }
        bool IsOnline { get; }
        bool IsSupporter { get; }
        DateTimeOffset? LastVisit { get; }
        bool PmFriendsOnly { get; }
        string ProfileColour { get; }
        string Username { get; }
        IReadOnlyCollection<IUserAccountHistory> AccountHistory { get; }
        IReadOnlyCollection<IUserProfileBanner> TournamentBanner { get; }
        IReadOnlyCollection<IUserBadge> Badges { get; }
        long? BeatmapPlaycountsCount { get; }
        object Blocks { get; }
        IUserCountry Country { get; }
        IUserCover Cover { get; }
        object CurrentModeRank { get; }
        long? FavouriteBeatmapsetCount { get; }
        long? GraveyardBeatmapsetCount { get; }
        long? FollowerCount { get; }
        object Friends { get; }
        IReadOnlyCollection<IUserGroup> Groups { get; }
        bool? IsAdmin { get; }
        bool? IsBng { get; }
        bool? IsFullBng { get; }
        bool? IsGmt { get; }
        bool? IsLimitedBn { get; }
        bool? IsModerator { get; }
        bool? IsNat { get; }
        bool? IsRestricted { get; }
        bool? IsSilenced { get; }
        long? LovedBeatmapsetCount { get; }
        IReadOnlyCollection<IUserMonthlyPlayCount> MonthlyPlaycounts { get; }
        IUserPage Page { get; }
        IReadOnlyCollection<string> PreviousUsernames { get; }
        long? RankedAndApprovedBeatmapsetCount { get; }
        IReadOnlyCollection<IUserMonthlyPlayCount> ReplayWatchedCounts { get; }
        long? ScoresBestCount { get; }
        long? ScoresFirstCount { get; }
        long? ScoresRecentCount { get; }
        IUserStatistics Statistics { get; }
        long? SupportLevel { get; }
        long? UnrankedBeatmapsetCount { get; }
        long? UnreadPmCount { get; }
        IReadOnlyCollection<IUserAchievement> UserAchievements { get; }
        IUserRankHistory RankHistory { get; }
    }
}