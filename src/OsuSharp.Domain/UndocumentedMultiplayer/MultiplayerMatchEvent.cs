using System;

namespace OsuSharp.Interfaces;

public class MultiplayerMatchEvent : IMultiplayerMatchEvent
{
    public long Id { get; set; }
    public IMultiplayerMatchEventDetails Detail { get; set; } = null!;
    public DateTimeOffset Timestamp { get; set; }
    public long? UserId { get; set; }
    public IMultiplayerMatchEventGame? Game { get; set; }

    internal MultiplayerMatchEvent()
    {
        
    }
}