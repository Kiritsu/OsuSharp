using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public class UserCompact
    {
        [JsonProperty("avatar_url")]
        public Uri AvatarUrl { get; internal set; }
        
        [JsonProperty("country_code")]
        public string CountryCode { get; internal set; }
        
        [JsonProperty("default_group")]
        public string DefaultGroup { get; internal set; }
        
        [JsonProperty("id")]
        public long Id { get; internal set; }
        
        [JsonProperty("is_active")]
        public bool IsActive { get; internal set; }
        
        [JsonProperty("is_bot")]
        public bool IsBot { get; internal set; }
        
        [JsonProperty("is_online")]
        public bool IsOnline { get; internal set; }
        
        [JsonProperty("is_supporter")]
        public bool IsSupporter { get; internal set; }
        
        [JsonProperty("last_visit", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? LastVisit { get; internal set; }
        
        [JsonProperty("pm_friends_only")]
        public bool PmFriendsOnly { get; internal set; }
        
        [JsonProperty("profile_colour")]
        public string ProfileColour { get; internal set; }
        
        [JsonProperty("username")]
        public string Username { get; internal set; }

        [JsonProperty("account_history")]
        public IReadOnlyCollection<UserAccountHistory> AccountHistory { get; internal set; }
        
        [JsonProperty("active_tournament_banner")]
        public IReadOnlyCollection<UserProfileBanner> TournamentBanner { get; internal set; }
        
        [JsonProperty("badges")]
        public IReadOnlyCollection<UserBadge> Badges { get; internal set; }
        
        [JsonProperty("beatmap_playcounts_count")]
        public long? BeatmapPlaycountsCount { get; internal set; }
        
        // todo: type
        [JsonProperty("blocks")]
        public object Blocks { get; internal set; }
        
        [JsonProperty("country")]
        public UserCountry Country { get; internal set; }
        
        [JsonProperty("cover")]
        public UserCover Cover { get; internal set; }
        
        // todo: type
        [JsonProperty("current_mode_rank")]
        public object CurrentModeRank { get; internal set; }
        
        [JsonProperty("favourite_beatmapset_count")]
        public long? FavouriteBeatmapsetCount { get; internal set; }
        
        [JsonProperty("graveyard_beatmapset_count")]
        public long? GraveyardBeatmapsetCount { get; internal set; }
        
        [JsonProperty("follower_count")]
        public long? FollowerCount { get; internal set; }
        
        // todo: type
        [JsonProperty("friends")]
        public object Friends { get; internal set; }
        
        [JsonProperty("groups")]
        public IReadOnlyCollection<UserGroup> Groups { get; internal set; }

        [JsonProperty("is_admin")]
        public bool? IsAdmin { get; internal set; }
        
        [JsonProperty("is_bng")]
        public bool? IsBng { get; internal set; }
        
        [JsonProperty("is_full_bn")]
        public bool? IsFullBng { get; internal set; }
        
        [JsonProperty("is_gmt")]
        public bool? IsGmt { get; internal set; }
        
        [JsonProperty("is_limited_bn")]
        public bool? IsLimitedBn { get; internal set; }
        
        [JsonProperty("is_moderator")]
        public bool? IsModerator { get; internal set; }
        
        [JsonProperty("is_nat")]
        public bool? IsNat { get; internal set; }
        
        [JsonProperty("is_restricted")]
        public bool? IsRestricted { get; internal set; }
        
        [JsonProperty("is_silenced")]
        public bool? IsSilenced { get; internal set; }
        
        [JsonProperty("loved_beatmapset_count")]
        public long? LovedBeatmapsetCount { get; internal set; }
        
        [JsonProperty("monthly_playcounts")]
        public IReadOnlyCollection<UserMonthlyPlayCount> MonthlyPlaycounts { get; internal set; }
        
        [JsonProperty("page")]
        public UserPage Page { get; set; }
        
        [JsonProperty("previous_usernames")]
        public IReadOnlyCollection<string> PreviousUsernames { get; internal set; }
        
        [JsonProperty("ranked_and_approved_beatmapset_count")]
        public long? RankedAndApprovedBeatmapsetCount { get; internal set; }
        
        [JsonProperty("replays_watched_count")]
        public IReadOnlyCollection<UserMonthlyPlayCount> ReplayWatchedCounts { get; internal set; }
        
        [JsonProperty("scores_best_count")]
        public long? ScoresBestCount { get; internal set; }

        [JsonProperty("scores_first_count")]
        public long? ScoresFirstCount { get; internal set; }

        [JsonProperty("scores_recent_count")]
        public long? ScoresRecentCount { get; internal set; }
        
        [JsonProperty("statistics")]
        public UserStatistics Statistics { get; internal set; }
        
        [JsonProperty("support_level")]
        public long? SupportLevel { get; internal set; }
        
        [JsonProperty("unranked_beatmapset_count")]
        public long? UnrankedBeatmapsetCount { get; internal set; }
        
        [JsonProperty("unread_pm_count")]
        public long? UnreadPmCount { get; internal set; }
        
        [JsonProperty("user_achievements")]
        public IReadOnlyCollection<UserAchievement> UserAchievements { get; internal set; }

        [JsonProperty("rank_history")]
        public UserRankHistory RankHistory { get; internal set; }

        internal UserCompact()
        {
            
        }
    }
}