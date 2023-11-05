using System;

namespace OsuSharp.Interfaces;

public interface IMultiplayerMatchUser
{
    string AvatarUrl { get; set; }
    string CountryCode { get; set; }
    string? DefaultGroup { get; set; }
    long Id { get; set; }
    bool IsActive { get; set; }
    bool IsBot { get; set; }
    bool IsDeleted { get; set; }
    bool IsOnline { get; set; }
    bool IsSupporter { get; set; }
    DateTimeOffset? LastVisit { get; set; }
    bool PmFriendsOnly { get; set; }
    string? ProfileColour { get; set; }
    string Username { get; set; }
    IUserCountry Country { get; set; }
}