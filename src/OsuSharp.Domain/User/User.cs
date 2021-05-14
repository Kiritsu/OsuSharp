using System;
using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class User : UserCompact, IUser
    {
        public string CoverUrl { get; internal set; }

        public string Discord { get; internal set; }

        public bool HasSupported { get; internal set; }

        public string Interests { get; internal set; }

        public DateTimeOffset JoinDate { get; internal set; }

        public IUserKudosu Kudosu { get; internal set; }

        public string Location { get; internal set; }

        public long MaxBlocks { get; internal set; }

        public long MaxFriends { get; internal set; }

        public string Occupation { get; internal set; }

        public GameMode GameMode { get; internal set; }

        public IReadOnlyCollection<string> Playstyle { get; internal set; }

        public long PostCount { get; internal set; }

        public string Skype { get; internal set; }

        public string Title { get; internal set; }

        public string Twitter { get; internal set; }

        public string Website { get; internal set; }

        internal User()
        {
            
        }
    }
}