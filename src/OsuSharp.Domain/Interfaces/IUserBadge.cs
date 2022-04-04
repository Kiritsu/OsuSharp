using System;

namespace OsuSharp.Interfaces;

public interface IUserBadge
{
    DateTimeOffset AwardedAt { get; }
    string Description { get; }
    string ImageUrl { get; }
    string Url { get; }
}