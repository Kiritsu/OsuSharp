using System;

namespace OsuSharp.Interfaces;

public interface IUserCompact : IClientEntity
{
    Uri AvatarUrl { get; }
    string CountryCode { get; }
    string DefaultGroup { get; }
    long Id { get; }
    bool IsActive { get; }
    bool IsBot { get; }
    bool IsOnline { get; }
    bool IsSupporter { get; }
    DateTimeOffset? LastVisit { get; }
    bool PmFriendsOnly { get; }
    string ProfileColour { get; }
    string Username { get; }
}