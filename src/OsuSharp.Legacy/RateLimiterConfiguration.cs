using System;

namespace OsuSharp.Legacy;

public sealed class RateLimiterConfiguration
{
    /// <summary>
    ///     Amount of requests before considering reaching the ratelimits. Defaults to 1200.
    /// </summary>
    public int MaxRequest { get; set; } = 1200;

    /// <summary>
    ///     Amount of time to wait after reaching the ratelimits. Defaults to a minute.
    /// </summary>
    public TimeSpan Interval { get; set; } = TimeSpan.FromMinutes(1);

    /// <summary>
    ///     Defines wether the ratelimiter must throw if ratelimits are hit. Defaults to false.
    /// </summary>
    public bool ThrowOnRatelimitHit { get; set; }
}