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
        public Optional<IReadOnlyCollection<UserAccountHistory>> AccountHistory { get; internal set; }
        
        [JsonProperty("active_tournament_banner")]
        public Optional<IReadOnlyCollection<UserProfileBanner>> TournamentBanner { get; internal set; }
        
        [JsonProperty("badges")]
        public Optional<IReadOnlyCollection<UserBadge>> Badges { get; internal set; }
        
        [JsonProperty("beatmap_playcounts_count")]
        public Optional<long> BeatmapPlaycountsCount { get; internal set; }
        
        // todo: type
        [JsonProperty("blocks")]
        public Optional<object> Blocks { get; internal set; }
        
        [JsonProperty("country")]
        public Optional<UserCountry> Country { get; internal set; }
        
        [JsonProperty("cover")]
        public Optional<UserCover> Cover { get; internal set; }
        
        // todo: type
        [JsonProperty("current_mode_rank")]
        public Optional<object> CurrentModeRank { get; internal set; }
        
        [JsonProperty("favourite_beatmapset_count")]
        public Optional<long> FavouriteBeatmapsetCount { get; internal set; }
        
        [JsonProperty("graveyard_beatmapset_count")]
        public Optional<long> GraveyardBeatmapsetCount { get; internal set; }
        
        [JsonProperty("follower_count")]
        public Optional<long> FollowerCount { get; internal set; }
        
        // todo: type
        [JsonProperty("friends")]
        public Optional<object> Friends { get; internal set; }
        
        [JsonProperty("groups")]
        public Optional<IReadOnlyCollection<UserGroup>> Groups { get; internal set; }

        [JsonProperty("is_admin")]
        public Optional<bool> IsAdmin { get; internal set; }
        
        [JsonProperty("is_bng")]
        public Optional<bool> IsBng { get; internal set; }
        
        [JsonProperty("is_full_bn")]
        public Optional<bool> IsFullBng { get; internal set; }
        
        [JsonProperty("is_gmt")]
        public Optional<bool> IsGmt { get; internal set; }
        
        [JsonProperty("is_limited_bn")]
        public Optional<bool> IsLimitedBn { get; internal set; }
        
        [JsonProperty("is_moderator")]
        public Optional<bool> IsModerator { get; internal set; }
        
        [JsonProperty("is_nat")]
        public Optional<bool> IsNat { get; internal set; }
        
        [JsonProperty("is_restricted")]
        public Optional<bool> IsRestricted { get; internal set; }
        
        [JsonProperty("is_silenced")]
        public Optional<bool> IsSilenced { get; internal set; }
        
        [JsonProperty("loved_beatmapset_count")]
        public Optional<long> LovedBeatmapsetCount { get; internal set; }
        
        [JsonProperty("monthly_playcounts")]
        public Optional<IReadOnlyCollection<UserMonthlyPlayCount>> MonthlyPlaycounts { get; internal set; }
        
        [JsonProperty("page")]
        public Optional<UserPage> Page { get; set; }
        
        [JsonProperty("previous_usernames")]
        public Optional<IReadOnlyCollection<string>> PreviousUsernames { get; internal set; }
        
        [JsonProperty("ranked_and_approved_beatmapset_count")]
        public Optional<long> RankedAndApprovedBeatmapsetCount { get; internal set; }
        
        [JsonProperty("replays_watched_count")]
        public Optional<IReadOnlyCollection<UserMonthlyPlayCount>> ReplayWatchedCounts { get; internal set; }
        
        [JsonProperty("scores_best_count")]
        public Optional<long> ScoresBestCount { get; internal set; }

        [JsonProperty("scores_first_count")]
        public Optional<long> ScoresFirstCount { get; internal set; }

        [JsonProperty("scores_recent_count")]
        public Optional<long> ScoresRecentCount { get; internal set; }
        
        [JsonProperty("statistics")]
        public Optional<UserStatistics> Statistics { get; internal set; }
        
        [JsonProperty("support_level")]
        public Optional<long> SupportLevel { get; internal set; }
        
        [JsonProperty("unranked_beatmapset_count")]
        public Optional<long> UnrankedBeatmapsetCount { get; internal set; }
        
        [JsonProperty("unread_pm_count")]
        public Optional<long> UnreadPmCount { get; internal set; }
        
        [JsonProperty("user_achievements")]
        public Optional<IReadOnlyCollection<UserAchievement>> UserAchievements { get; internal set; }

        [JsonProperty("rank_history")]
        public Optional<UserRankHistory> RankHistory { get; internal set; }

        internal UserCompact()
        {
            
        }
    }
}