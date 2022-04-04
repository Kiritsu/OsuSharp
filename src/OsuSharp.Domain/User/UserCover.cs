using System;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class UserCover : IUserCover
{
    public string CustomUrl { get; internal set; } = null!;

    public Uri Url { get; internal set; } = null!;

    public string Id { get; internal set; } = null!;

    internal UserCover()
    {
            
    }
}