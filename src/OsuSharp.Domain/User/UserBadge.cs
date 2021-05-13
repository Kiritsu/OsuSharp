using System;

namespace OsuSharp.Domain
{
    public sealed class UserBadge
    {
        public DateTimeOffset AwardedAt { get; internal set; }

        public string Description { get; internal set; }

        public string ImageUrl { get; internal set; }

        public string Url { get; internal set; }

        internal UserBadge()
        {
            
        }
    }
}