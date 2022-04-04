using System;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public abstract class Event : IEvent
{
    public DateTimeOffset CreatedAt { get; internal set; }

    public long Id { get; internal set; }

    public EventType Type { get; internal set; }

    public IOsuClient Client { get; internal set; } = null!;

    internal Event()
    {
            
    }
}