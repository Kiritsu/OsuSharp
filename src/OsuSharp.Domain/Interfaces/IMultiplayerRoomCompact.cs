using System;

namespace OsuSharp.Interfaces;

public interface IMultiplayerRoomCompact
{
    /// <summary>
    ///     Gets the multiplayer match id.
    /// </summary>
    long MatchId { get; }

    /// <summary>
    ///     Gets the name of the multiplayer match.
    /// </summary>
    string Name { get; }

    /// <summary>
    ///     Gets the date time when the match created.
    /// </summary>
    DateTimeOffset StartTime { get; }

    /// <summary>
    ///     Gets the date time when the match ended.
    /// </summary>
    DateTimeOffset? EndTime { get; }
}