using System;

namespace OsuSharp.Interfaces;

public interface IMultiplayerMatchEvent
{
    long Id { get; }
    IMultiplayerMatchEventDetails Detail { get; }
    DateTimeOffset Timestamp { get; }
    long? UserId { get; }
    IMultiplayerMatchEventGame? Game { get; }
}