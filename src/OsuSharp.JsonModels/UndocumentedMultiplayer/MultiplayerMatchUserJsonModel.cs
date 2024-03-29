using System;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class MultiplayerMatchUserJsonModel : JsonModel
{
    [JsonProperty("avatar_url")]
    public string AvatarUrl { get; set; } = null!;
    
    [JsonProperty("country_code")]
    public string CountryCode { get; set; } = null!;
    
    [JsonProperty("default_group")]
    public string? DefaultGroup { get; set; }
    
    [JsonProperty("id")]
    public long Id { get; set; }
    
    [JsonProperty("is_active")]
    public bool IsActive { get; set; }
    
    [JsonProperty("is_bot")]
    public bool IsBot { get; set; }
    
    [JsonProperty("is_deleted")]
    public bool IsDeleted { get; set; }
    
    [JsonProperty("is_online")]
    public bool IsOnline { get; set; }
    
    [JsonProperty("is_supporter")]
    public bool IsSupporter { get; set; }
    
    [JsonProperty("last_visit")]
    public DateTimeOffset? LastVisit { get; set; }
    
    [JsonProperty("pm_friends_only")]
    public bool PmFriendsOnly { get; set; }

    [JsonProperty("profile_colour")]
    public string? ProfileColour { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; } = null!;

    [JsonProperty("country")]
    public UserCountryJsonModel Country { get; set; } = null!;
}