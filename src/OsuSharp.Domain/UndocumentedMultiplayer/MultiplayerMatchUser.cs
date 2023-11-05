using System;

namespace OsuSharp.Interfaces;

public class MultiplayerMatchUser : IMultiplayerMatchUser
{
    public string AvatarUrl { get; set; } = null!;
    public string CountryCode { get; set; } = null!;
    public string? DefaultGroup { get; set; }
    public long Id { get; set; }
    public bool IsActive { get; set; }
    public bool IsBot { get; set; }
    public bool IsDeleted { get; set; }
    public bool IsOnline { get; set; }
    public bool IsSupporter { get; set; }
    public DateTimeOffset? LastVisit { get; set; }
    public bool PmFriendsOnly { get; set; }
    public string? ProfileColour { get; set; }
    public string Username { get; set; } = null!;
    public IUserCountry Country { get; set; } = null!;

    internal MultiplayerMatchUser()
    {
        
    }
}