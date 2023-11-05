using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class MultiplayerHistoryPageJsonModel : JsonModel
{
    [JsonProperty("cursor_string")] 
    public string CursorString { get; set; } = null!;

    [JsonProperty("params")] 
    public MultiplayerHistoryParamsJsonModel Params { get; set; } = null!;

    [JsonProperty("cursor")] 
    public Dictionary<string, string> Cursor { get; set; } = null!;

    [JsonProperty("matches")] 
    public List<MultiplayerRoomCompactJsonModel> Matches { get; set; } = null!;
}