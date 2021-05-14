using System;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class UserCover : IUserCover
    {
        public string CustomUrl { get; internal set; }

        public Uri Url { get; internal set; }

        public string Id { get; internal set; }

        internal UserCover()
        {
            
        }
    }
}