using System.Collections.Generic;

namespace OsuSharp.Interfaces;

public class MultiplayerMatch : IMultiplayerMatch
{
    public IMultiplayerRoomCompact Match { get; set; } = null!;
    public IReadOnlyList<IMultiplayerMatchEvent> Events { get; set; } = null!;
    public IReadOnlyList<IMultiplayerMatchUser> Users { get; set; } = null!;
    public long FirstEventId { get; set; }
    public long LatestEventId { get; set; }
    public long? CurrentGameId { get; set; }

    internal MultiplayerMatch()
    {
        
    }
}