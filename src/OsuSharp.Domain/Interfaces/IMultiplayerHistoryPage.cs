using System.Collections.Generic;

namespace OsuSharp.Interfaces;

public interface IMultiplayerHistoryPage
{
    string CursorString { get; set; }
    IMultiplayerHistoryParams Params { get; set; }
    
    //TODO: change it to more discrete type
    IReadOnlyDictionary<string, string> Cursor { get; set; }
    IReadOnlyList<IMultiplayerRoomCompact> Matches { get; set; }
}