using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public class UserCompact
    {
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; internal set; }
        
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
        
        // todo: type
        [JsonProperty("country")]
        public Optional<object> Country { get; internal set; }
        
        // todo: type
        [JsonProperty("cover")]
        public Optional<object> Cover { get; internal set; }
        
        // todo: type
        [JsonProperty("current_mode_rank")]
        public Optional<object> CurrentModeRank { get; internal set; }
        
        [JsonProperty("favourite_beatmapset_count")]
        public Optional<long> FavouriteBeatmapsetCount { get; internal set; }
        
        [JsonProperty("graveyard_beatmapset_count")]
        public Optional<long> GraveyardBeatmapsetCount { get; internal set; }
        
        // todo: type
        [JsonProperty("friends")]
        public Optional<object> Friends { get; internal set; }

        internal UserCompact()
        {
            
        }
    }

    public sealed class UserAccountHistory
    {
        [JsonProperty("id")]
        public long Id { get; internal set; }
        
        [JsonProperty("type")]
        public string Type { get; internal set; }
        
        [JsonProperty("timestamp")]
        public DateTimeOffset TimeStamp { get; internal set; }
        
        [JsonProperty("length")]
        public int Length { get; internal set; }
    }

    public sealed class UserProfileBanner
    {
        [JsonProperty("id")]
        public long Id { get; internal set; }
        
        [JsonProperty("tournament_id")]
        public long TournamentId { get; internal set; }
        
        [JsonProperty("image")]
        public string Image { get; internal set; }
    }

    public sealed class UserBadge
    {
        [JsonProperty("awarded_at")]
        public DateTimeOffset AwardedAt { get; internal set; }
        
        [JsonProperty("description")]
        public string Description { get; internal set; }
        
        [JsonProperty("image_url")]
        public string ImageUrl { get; internal set; }
        
        [JsonProperty("url")]
        public string Url { get; internal set; }
    }
}