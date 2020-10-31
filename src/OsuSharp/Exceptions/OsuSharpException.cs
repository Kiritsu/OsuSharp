﻿using System;

namespace OsuSharp.Exceptions
{
    public class PreemptiveRateLimitException : Exception
    {
        /// <summary>
        ///     Gets the time before the ratelimit bucket has expired.
        /// </summary>
        public TimeSpan ExpiresIn { get; internal set; }

        internal PreemptiveRateLimitException() : base("Preemptive ratelimits reached. Retry later.")
        {
            
        }
    }
}