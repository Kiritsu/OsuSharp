using System;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface IEvent : IClientEntity
{
    DateTimeOffset CreatedAt { get; }
    long Id { get; }
    EventType Type { get; }
}