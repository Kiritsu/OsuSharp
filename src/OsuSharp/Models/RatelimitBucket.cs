using System;

namespace OsuSharp.Models;

internal sealed class RatelimitBucket
{
    public int Remaining { get; set; } = 60;

    public int Limit { get; set; } = 60;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

    public TimeSpan Duration { get; set; } = TimeSpan.FromSeconds(60);

    public TimeSpan ExpiresIn => Duration - (DateTimeOffset.Now - CreatedAt);

    public bool HasExpired => ExpiresIn < TimeSpan.Zero;
}