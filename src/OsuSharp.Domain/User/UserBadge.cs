using System;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class UserBadge : IUserBadge
{
    public DateTimeOffset AwardedAt { get; internal set; }

    public string Description { get; internal set; } = null!;

    public string ImageUrl { get; internal set; } = null!;

    public string Url { get; internal set; } = null!;

    internal UserBadge()
    {
            
    }
}