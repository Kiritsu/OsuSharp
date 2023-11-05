using System;

namespace OsuSharp.Interfaces;

public interface IMultiplayerMatchEvent
{
    long Id { get; set; }
    IMultiplayerMatchEventDetails Detail { get; set; }
    DateTimeOffset Timestamp { get; set; }
    long? UserId { get; set; }
    IMultiplayerMatchEventGame? Game { get; set; }
}