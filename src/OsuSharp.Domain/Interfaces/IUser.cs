﻿using System;
using System.Collections.Generic;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces
{
    public interface IUser : IUserCompact
    {
        string Discord { get; }
        bool HasSupported { get; }
        string Interests { get; }
        DateTimeOffset JoinDate { get; }
        IUserKudosu Kudosu { get; }
        string Location { get; }
        long MaxBlocks { get; }
        long MaxFriends { get; }
        string Occupation { get; }
        GameMode GameMode { get; }
        IReadOnlyList<string> Playstyle { get; }
        long PostCount { get; }
        string Skype { get; }
        string Title { get; }
        string Twitter { get; }
        string Website { get; }
    }
}