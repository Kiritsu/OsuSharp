using System;

namespace OsuSharp.Exceptions;

/// <summary>
/// Represents an exception that occured when the rate limits would have been reached.
/// </summary>
public class PreemptiveRateLimitException : Exception
{
    /// <summary>
    /// Gets the time before the ratelimit bucket has expired.
    /// </summary>
    public TimeSpan ExpiresIn { get; }

    internal PreemptiveRateLimitException(TimeSpan expiresIn) : 
        base($"Preemptive rate-limits reached. Retry in {(int)expiresIn.TotalSeconds} seconds.")
    {
        ExpiresIn = expiresIn;
    }
}