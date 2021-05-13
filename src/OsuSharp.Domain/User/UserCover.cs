using System;

namespace OsuSharp.Domain
{
    public sealed class UserCover
    {
        public string CustomUrl { get; internal set; }

        public Uri Url { get; internal set; }

        public string Id { get; internal set; }

        internal UserCover()
        {
            
        }
    }
}