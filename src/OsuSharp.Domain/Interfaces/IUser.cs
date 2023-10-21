using System.Collections.Generic;

namespace OsuSharp.Interfaces;

public interface IUser : IUserCompact
{
    IReadOnlyList<IUserAccountHistory> AccountHistory { get; }
    IReadOnlyList<IUserProfileBanner> TournamentBanner { get; }
    IReadOnlyList<IUserBadge> Badges { get; }
    long? BeatmapPlaycountsCount { get; }
    long? UnrankedBeatmapsetCount { get; }
    long? RankedAndApprovedBeatmapsetCount { get; }
    long? ScoresPinnedCount { get; }
    string CoverUrl { get; }
    object Blocks { get; }
    IUserCountry Country { get; }
    IUserCover Cover { get; }
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
    long? RankedBeatmapsetCount { get; }
    IReadOnlyList<IUserMonthlyPlayCount> ReplaysWatchedCounts { get; }
    long? ScoresBestCount { get; }
    long? ScoresFirstCount { get; }
    long? ScoresRecentCount { get; }
    IUserStatistics Statistics { get; }
    long? SupportLevel { get; }
    long? PendingBeatmapsetCount { get; }
    long? UnreadPmCount { get; }
    IReadOnlyList<IUserAchievement> UserAchievements { get; }
    IUserRankHistory RankHistory { get; }
    long? CommentsCount { get; }
    bool? IsDeleted { get; }
    List<string> ProfileOrder { get; }
    string TitleUrl { get; }
    long? MappingFollowerCount { get; }
}