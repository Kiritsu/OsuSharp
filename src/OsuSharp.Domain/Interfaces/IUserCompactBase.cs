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
        IReadOnlyList<IUserAccountHistory> AccountHistory { get; }
        IReadOnlyList<IUserProfileBanner> TournamentBanner { get; }
        IReadOnlyList<IUserBadge> Badges { get; }
        long? BeatmapPlaycountsCount { get; }
        object Blocks { get; }
        IUserCountry Country { get; }
        IUserCover Cover { get; }
        object CurrentModeRank { get; }
        long? FavouriteBeatmapsetCount { get; }
        long? GraveyardBeatmapsetCount { get; }
        long? FollowerCount { get; }
        object Friends { get; }
        IReadOnlyList<IUserGroup> Groups { get; }
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
        IReadOnlyList<IUserMonthlyPlayCount> MonthlyPlaycounts { get; }
        IUserPage Page { get; }
        IReadOnlyList<string> PreviousUsernames { get; }
        long? RankedAndApprovedBeatmapsetCount { get; }
        IReadOnlyList<IUserMonthlyPlayCount> ReplayWatchedCounts { get; }
        long? ScoresBestCount { get; }
        long? ScoresFirstCount { get; }
        long? ScoresRecentCount { get; }
        IUserStatistics Statistics { get; }
        long? SupportLevel { get; }
        long? UnrankedBeatmapsetCount { get; }
        long? UnreadPmCount { get; }
        IReadOnlyList<IUserAchievement> UserAchievements { get; }
        IUserRankHistory RankHistory { get; }
    }
}