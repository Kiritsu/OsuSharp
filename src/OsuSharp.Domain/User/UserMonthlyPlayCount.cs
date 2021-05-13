using System;

namespace OsuSharp.Domain
{
    public sealed class UserMonthlyPlayCount
    {
        public DateTimeOffset StartDate { get; internal set; }

        public long Count { get; internal set; }

        internal UserMonthlyPlayCount()
        {
            
        }
    }
}