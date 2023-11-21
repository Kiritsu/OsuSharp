using System.Collections.Generic;

namespace OsuSharp.Interfaces;

public interface IMultiplayerHistoryPage
{
    string CursorString { get; }
    IMultiplayerHistoryParams Params { get; }
    
    //TODO: change it to more discrete type
    IReadOnlyDictionary<string, string> Cursor { get; }
    IReadOnlyList<IMultiplayerRoomCompact> Matches { get; }
}