using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class UserCompactJsonModel : JsonModel
    {
        [JsonProperty("avatar_url")]
        public Uri AvatarUrl { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("default_group")]
        public string DefaultGroup { get; set; }

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
        public string ProfileColour { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        [JsonProperty("account_history")]
        public IReadOnlyCollection<UserAccountHistoryJsonModel> AccountHistory { get; set; }

        [JsonProperty("active_tournament_banner")]
        public IReadOnlyCollection<UserProfileBannerJsonModel> TournamentBanner { get; set; }

        [JsonProperty("badges")]
        public IReadOnlyCollection<UserBadgeJsonModel> Badges { get; set; }

        [JsonProperty("beatmap_playcounts_count")]
        public long? BeatmapPlaycountsCount { get; set; }

        // todo: type
        [JsonProperty("blocks")]
        public object Blocks { get; set; }

        [JsonProperty("country")]
        public UserCountryJsonModel CountryJsonModel { get; set; }

        [JsonProperty("cover")]
        public UserCoverJsonModel CoverJsonModel { get; set; }

        // todo: type
        [JsonProperty("current_mode_rank")]
        public object CurrentModeRank { get; set; }

        [JsonProperty("favourite_beatmapset_count")]
        public long? FavouriteBeatmapsetCount { get; set; }

        [JsonProperty("graveyard_beatmapset_count")]
        public long? GraveyardBeatmapsetCount { get; set; }

        [JsonProperty("follower_count")]
        public long? FollowerCount { get; set; }

        // todo: type
        [JsonProperty("friends")]
        public object Friends { get; set; }

        [JsonProperty("groups")]
        public IReadOnlyCollection<UserGroupJsonModel> Groups { get; set; }

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
        public IReadOnlyCollection<UserMonthlyPlayCountJsonModel> MonthlyPlaycounts { get; set; }

        [JsonProperty("page")]
        public UserPageJsonModel PageJsonModel { get; set; }

        [JsonProperty("previous_usernames")]
        public IReadOnlyCollection<string> PreviousUsernames { get; set; }

        [JsonProperty("ranked_and_approved_beatmapset_count")]
        public long? RankedAndApprovedBeatmapsetCount { get; set; }

        [JsonProperty("replays_watched_count")]
        public IReadOnlyCollection<UserMonthlyPlayCountJsonModel> ReplayWatchedCounts { get; set; }

        [JsonProperty("scores_best_count")]
        public long? ScoresBestCount { get; set; }

        [JsonProperty("scores_first_count")]
        public long? ScoresFirstCount { get; set; }

        [JsonProperty("scores_recent_count")]
        public long? ScoresRecentCount { get; set; }

        [JsonProperty("statistics")]
        public UserStatisticsJsonModel StatisticsJsonModel { get; set; }

        [JsonProperty("support_level")]
        public long? SupportLevel { get; set; }

        [JsonProperty("unranked_beatmapset_count")]
        public long? UnrankedBeatmapsetCount { get; set; }

        [JsonProperty("unread_pm_count")]
        public long? UnreadPmCount { get; set; }

        [JsonProperty("user_achievements")]
        public IReadOnlyCollection<UserAchievementJsonModel> UserAchievements { get; set; }

        [JsonProperty("rank_history")]
        public UserRankHistoryJsonModel RankHistoryJsonModel { get; set; }
    }
}