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

        [JsonProperty("account_history", NullValueHandling = NullValueHandling.Ignore)]
        public Optional<IReadOnlyCollection<UserAccountHistory>> AccountHistory { get; internal set; }
        
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
}