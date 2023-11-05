using System;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class MultiplayerMatchEventJsonModel : JsonModel
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("detail")] 
    public MultiplayerMatchEventDetailsJsonModel Detail { get; set; } = null!;
    
    [JsonProperty("timestamp")]
    public DateTimeOffset Timestamp { get; set; }
    
    [JsonProperty("user_id")]
    public long? UserId { get; set; }
    
    [JsonProperty("game")] 
    public MultiplayerMatchEventGameJsonModel? Game { get; set; }
}
