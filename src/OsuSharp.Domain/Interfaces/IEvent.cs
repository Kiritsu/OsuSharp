using System;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces
{
    public interface IEvent
    {
        DateTimeOffset CreatedAt { get; }
        long Id { get; }
        EventType Type { get; }
    }
}