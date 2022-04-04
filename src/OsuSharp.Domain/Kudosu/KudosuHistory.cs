using System;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class KudosuHistory : IKudosuHistory
{
    public long Id { get; internal set; }

    public KudosuAction Action { get; internal set; }

    public long Amount { get; internal set; }

    //todo: make enum
    public string Model { get; internal set; } = null!;

    public DateTimeOffset CreatedAt { get; internal set; }

    public IKudosuGiver Giver { get; internal set; } = null!;

    public IKudosuPost Post { get; internal set; } = null!;

    public IOsuClient Client { get; internal set; } = null!;

    internal KudosuHistory()
    {
            
    }
}