using System.Collections.Generic;

namespace OsuSharp.Interfaces;

public class MultiplayerHistoryPage : IMultiplayerHistoryPage
{
    public string CursorString { get; set; } = null!;
    public IMultiplayerHistoryParams Params { get; set; } = null!;
    public IReadOnlyDictionary<string, string> Cursor { get; set; } = null!;
    public IReadOnlyList<IMultiplayerRoomCompact> Matches { get; set; } = null!;

    internal MultiplayerHistoryPage()
    {
        
    }
}