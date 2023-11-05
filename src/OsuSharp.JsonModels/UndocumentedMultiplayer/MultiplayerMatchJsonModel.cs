using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class MultiplayerMatchJsonModel : JsonModel
{
    [JsonProperty("match")] 
    public MultiplayerRoomCompactJsonModel Match { get; set; } = null!;

    [JsonProperty("events")] 
    public List<MultiplayerMatchEventJsonModel> Events { get; set; } = null!;

    [JsonProperty("users")]
    public List<MultiplayerMatchUserJsonModel> Users { get; set; } = null!;
    
    [JsonProperty("first_event_id")]
    public long FirstEventId { get; set; }
    
    [JsonProperty("latest_event_id")]
    public long LatestEventId { get; set; }
    
    [JsonProperty("current_game_id")]
    public long? CurrentGameId { get; set; }
}