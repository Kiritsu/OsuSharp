using System;

namespace OsuSharp.Interfaces;

public class MultiplayerRoomCompact : IMultiplayerRoomCompact
{
    /// <summary>
    ///     Gets the multiplayer match id.
    /// </summary>
    public long MatchId { get; internal set; }

    /// <summary>
    ///     Gets the name of the multiplayer match.
    /// </summary>
    public string Name { get; internal set; } = null!;

    /// <summary>
    ///     Gets the date time when the match created.
    /// </summary>
    public DateTimeOffset StartTime { get; internal set; }

    /// <summary>
    ///     Gets the date time when the match ended.
    /// </summary>
    public DateTimeOffset? EndTime { get; internal set; }

    internal MultiplayerRoomCompact()
    {
        
    }
}