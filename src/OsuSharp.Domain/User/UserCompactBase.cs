using System;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public class UserCompactBase : IUserCompactBase
    {
        public Uri AvatarUrl { get; internal set; }
        public string CountryCode { get; internal set; }
        public string DefaultGroup { get; internal set; }
        public long Id { get; internal set; }
        public bool IsActive { get; internal set; }
        public bool IsBot { get; internal set; }
        public bool IsOnline { get; internal set; }
        public bool IsSupporter { get; internal set; }
        public DateTimeOffset? LastVisit { get; internal set; }
        public bool PmFriendsOnly { get; internal set; }
        public string ProfileColour { get; internal set; }
        public string Username { get; internal set; }

        internal UserCompactBase()
        {

        }
    }
}