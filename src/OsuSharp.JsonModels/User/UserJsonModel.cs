using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class UserJsonModel : JsonModel
{
    [JsonProperty("avatar_url")]
    public Uri AvatarUrl { get; set; } = null!;

    [JsonProperty("country_code")]
    public string CountryCode { get; set; } = null!;

    [JsonProperty("default_group")]
    public string DefaultGroup { get; set; } = null!;

    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("is_active")]
    public bool IsActive { get; set; }

    [JsonProperty("is_bot")]
    public bool IsBot { get; set; }

    [JsonProperty("is_online")]
    public bool IsOnline { get; set; }

    [JsonProperty("is_supporter")]
    public bool IsSupporter { get; set; }

    [JsonProperty("last_visit", NullValueHandling = NullValueHandling.Ignore)]
    public DateTimeOffset? LastVisit { get; set; }

    [JsonProperty("pm_friends_only")]
    public bool PmFriendsOnly { get; set; }

    [JsonProperty("profile_colour")]
    public string ProfileColour { get; set; } = null!;

    [JsonProperty("username")]
    public string Username { get; set; } = null!;

    [JsonProperty("account_history")] 
    public List<UserAccountHistoryJsonModel> AccountHistory { get; set; } = new();

    [JsonProperty("active_tournament_banner")]
    public List<UserProfileBannerJsonModel> TournamentBanner { get; set; } = new();

    [JsonProperty("badges")] 
    public List<UserBadgeJsonModel> Badges { get; set; } = new();

    [JsonProperty("beatmap_playcounts_count")]
    public long? BeatmapPlaycountsCount { get; set; }

    // todo: type
    [JsonProperty("blocks")]
    public object? Blocks { get; set; }

    [JsonProperty("country")]
    public UserCountryJsonModel Country { get; set; } = null!;

    [JsonProperty("cover")]
    public UserCoverJsonModel Cover { get; set; } = null!;

    // todo: type
    [JsonProperty("current_mode_rank")]
    public object? CurrentModeRank { get; set; }

    [JsonProperty("favourite_beatmapset_count")]
    public long? FavouriteBeatmapsetCount { get; set; }

    [JsonProperty("graveyard_beatmapset_count")]
    public long? GraveyardBeatmapsetCount { get; set; }

    [JsonProperty("follower_count")]
    public long? FollowerCount { get; set; }

    // todo: type
    [JsonProperty("friends")]
    public object? Friends { get; set; }

    [JsonProperty("groups")] 
    public List<UserGroupJsonModel> Groups { get; set; } = new();

    [JsonProperty("is_admin")]
    public bool? IsAdmin { get; set; }

    [JsonProperty("is_bng")]
    public bool? IsBng { get; set; }

    [JsonProperty("is_full_bn")]
    public bool? IsFullBng { get; set; }

    [JsonProperty("is_gmt")]
    public bool? IsGmt { get; set; }

    [JsonProperty("is_limited_bn")]
    public bool? IsLimitedBn { get; set; }

    [JsonProperty("is_moderator")]
    public bool? IsModerator { get; set; }

    [JsonProperty("is_nat")]
    public bool? IsNat { get; set; }

    [JsonProperty("is_restricted")]
    public bool? IsRestricted { get; set; }

    [JsonProperty("is_silenced")]
    public bool? IsSilenced { get; set; }

    [JsonProperty("loved_beatmapset_count")]
    public long? LovedBeatmapsetCount { get; set; }

    [JsonProperty("monthly_playcounts")] 
    public List<UserMonthlyPlayCountJsonModel> MonthlyPlaycounts { get; set; } = new();

    [JsonProperty("page")]
    public UserPageJsonModel Page { get; set; } = null!;

    [JsonProperty("previous_usernames")]
    public List<string> PreviousUsernames { get; set; } = null!;

    [JsonProperty("ranked_beatmapset_count")]
    public long? RankedBeatmapsetCount { get; set; }

    [JsonProperty("replays_watched_count")]
    public List<UserMonthlyPlayCountJsonModel> ReplayWatchedCounts { get; set; } = new();

    [JsonProperty("scores_best_count")]
    public long? ScoresBestCount { get; set; }

    [JsonProperty("scores_first_count")]
    public long? ScoresFirstCount { get; set; }

    [JsonProperty("scores_recent_count")]
    public long? ScoresRecentCount { get; set; }

    [JsonProperty("statistics")]
    public UserStatisticsJsonModel Statistics { get; set; } = null!;

    [JsonProperty("support_level")]
    public long? SupportLevel { get; set; }

    [JsonProperty("pending_beatmapset_count")]
    public long? PendingBeatmapsetCount { get; set; }

    [JsonProperty("unread_pm_count")]
    public long? UnreadPmCount { get; set; }

    [JsonProperty("user_achievements")]
    public List<UserAchievementJsonModel> UserAchievements { get; set; } = null!;

    [JsonProperty("rank_history")]
    public UserRankHistoryJsonModel RankHistory { get; set; } = null!;

    [JsonProperty("comments_count")]
    public long? CommentsCount { get; set; }

    [JsonProperty("is_deleted")]
    public bool? IsDeleted { get; set; }

    [JsonProperty("profile_order")]
    public List<string> ProfileOrder { get; set; } = new();

    [JsonProperty("title_url")]
    public string TitleUrl { get; set; } = null!;

    [JsonProperty("mapping_follower_count")]
    public long? MappingFollowerCount { get; set; }

    [JsonProperty("replays_watched_counts")]
    public List<UserMonthlyPlayCountJsonModel>? ReplaysWatchedCounts { get; set; } = null!;

    [JsonProperty("discord", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Discord { get; set; } = null!;

    [JsonProperty("has_supported")]
    public bool HasSupported { get; set; }

    [JsonProperty("interests", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Interests { get; set; } = null!;

    [JsonProperty("join_date")]
    public DateTimeOffset JoinDate { get; set; }

    [JsonProperty("kudosu")]
    public UserKudosuJsonModel Kudosu { get; set; } = null!;

    [JsonProperty("location", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Location { get; set; } = null!;

    [JsonProperty("max_blocks")]
    public long MaxBlocks { get; set; }

    [JsonProperty("max_friends")]
    public long MaxFriends { get; set; }

    [JsonProperty("occupation", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Occupation { get; set; } = null!;

    [JsonProperty("playmode")]
    public string GameMode { get; set; } = null!;

    [JsonProperty("playstyle")]
    public List<string> Playstyle { get; set; } = null!;

    [JsonProperty("post_count")]
    public long PostCount { get; set; }

    [JsonProperty("skype", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Skype { get; set; } = null!;

    [JsonProperty("title", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Title { get; set; } = null!;

    [JsonProperty("twitter", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Twitter { get; set; } = null!;

    [JsonProperty("website", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string Website { get; set; } = null!;
    
    [JsonProperty("unranked_beatmapset_count")]
    public long? UnrankedBeatmapsetCount { get; internal set; }
    
    [JsonProperty("scores_pinned_count")]
    public long? ScoresPinnedCount { get; internal set; }
    
    [JsonProperty("ranked_and_approved_beatmapset_count")]
    public long? RankedAndApprovedBeatmapsetCount { get; internal set; }

    [JsonProperty("cover_url")] 
    public string CoverUrl { get; internal set; } = null!;
}