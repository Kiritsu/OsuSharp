using System.Collections.Generic;

namespace OsuSharp.Interfaces;

public interface IMultiplayerMatch
{
    IMultiplayerRoomCompact Match { get; set; }
    IReadOnlyList<IMultiplayerMatchEvent> Events { get; set; }
    IReadOnlyList<IMultiplayerMatchUser> Users { get; set; }
    long FirstEventId { get; set; }
    long LatestEventId { get; set; }
    long? CurrentGameId { get; set; }
}
