using System;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public class UserCompact : IUserCompact
{
    public Uri AvatarUrl { get; internal set; } = null!;
    public string CountryCode { get; internal set; } = null!;
    public string DefaultGroup { get; internal set; } = null!;
    public long Id { get; internal set; }
    public bool IsActive { get; internal set; }
    public bool IsBot { get; internal set; }
    public bool IsOnline { get; internal set; }
    public bool IsSupporter { get; internal set; }
    public DateTimeOffset? LastVisit { get; internal set; }
    public bool PmFriendsOnly { get; internal set; }
    public string ProfileColour { get; internal set; } = null!;
    public string Username { get; internal set; } = null!;
    public IOsuClient Client { get; internal set; } = null!;

    internal UserCompact()
    {

    }
}