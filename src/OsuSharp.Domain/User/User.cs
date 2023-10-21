using OsuSharp.Interfaces;
using System.Collections.Generic;

namespace OsuSharp.Domain;

public class User : UserCompact, IUser
{
    public IReadOnlyList<IUserAccountHistory> AccountHistory { get; internal set; } = null!;
    public IReadOnlyList<IUserProfileBanner> TournamentBanner { get; internal set; } = null!;
    public IReadOnlyList<IUserBadge> Badges { get; internal set; } = null!;
    public long? BeatmapPlaycountsCount { get; internal set; }
    public object Blocks { get; internal set; } = null!;
    public IUserCountry Country { get; internal set; } = null!;
    public IUserCover Cover { get; internal set; } = null!;
    public string CoverUrl { get; internal set; } = null!;
    public long? FavouriteBeatmapsetCount { get; internal set; }
    public long? GraveyardBeatmapsetCount { get; internal set; }
    public long? UnrankedBeatmapsetCount { get; internal set; }
    public long? ScoresPinnedCount { get; internal set; }
    public long? RankedAndApprovedBeatmapsetCount { get; internal set; }
    public long? FollowerCount { get; internal set; }
    public object Friends { get; internal set; } = null!;
    public IReadOnlyList<IUserGroup> Groups { get; internal set; } = null!;
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
    public IReadOnlyList<IUserMonthlyPlayCount> MonthlyPlaycounts { get; internal set; } = null!;
    public IUserPage Page { get; set; } = null!;
    public IReadOnlyList<string> PreviousUsernames { get; internal set; } = null!;
    public long? RankedBeatmapsetCount { get; internal set; }
    public IReadOnlyList<IUserMonthlyPlayCount> ReplaysWatchedCounts { get; internal set; } = null!;
    public long? ScoresBestCount { get; internal set; }
    public long? ScoresFirstCount { get; internal set; }
    public long? ScoresRecentCount { get; internal set; }
    public IUserStatistics Statistics { get; internal set; } = null!;
    public long? SupportLevel { get; internal set; }
    public long? PendingBeatmapsetCount { get; internal set; }
    public long? UnreadPmCount { get; internal set; }
    public IReadOnlyList<IUserAchievement> UserAchievements { get; internal set; } = null!;
    public IUserRankHistory RankHistory { get; internal set; } = null!;
    public long? CommentsCount { get; set; }
    public bool? IsDeleted { get; set; }
    public List<string> ProfileOrder { get; set; } = null!;
    public string TitleUrl { get; set; } = null!;
    public long? MappingFollowerCount { get; set; }

    internal User()
    {
            
    }
}