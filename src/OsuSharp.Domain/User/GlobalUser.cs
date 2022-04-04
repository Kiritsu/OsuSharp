using System;
using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class GlobalUser : User, IGlobalUser
{
    public string Discord { get; internal set; } = null!;

    public bool HasSupported { get; internal set; }

    public string Interests { get; internal set; } = null!;

    public DateTimeOffset JoinDate { get; internal set; }

    public IUserKudosu Kudosu { get; internal set; } = null!;

    public string Location { get; internal set; } = null!;

    public long MaxBlocks { get; internal set; }

    public long MaxFriends { get; internal set; }

    public string Occupation { get; internal set; } = null!;

    public GameMode GameMode { get; internal set; }

    public IReadOnlyList<string> Playstyle { get; internal set; } = null!;

    public long PostCount { get; internal set; }

    public string Skype { get; internal set; } = null!;

    public string Title { get; internal set; } = null!;

    public string Twitter { get; internal set; } = null!;

    public string Website { get; internal set; } = null!;

    internal GlobalUser()
    {
            
    }
}