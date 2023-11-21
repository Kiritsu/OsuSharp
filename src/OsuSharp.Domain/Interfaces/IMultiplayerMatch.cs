using System.Collections.Generic;

namespace OsuSharp.Interfaces;

public interface IMultiplayerMatch
{
    IMultiplayerRoomCompact Match { get; }
    IReadOnlyList<IMultiplayerMatchEvent> Events { get; }
    IReadOnlyList<IUserCompact> Users { get; }
    long FirstEventId { get; }
    long LatestEventId { get; }
    long? CurrentGameId { get; }
}
