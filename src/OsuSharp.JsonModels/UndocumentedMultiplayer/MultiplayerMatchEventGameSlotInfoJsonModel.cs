using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class MultiplayerMatchEventGameSlotInfoJsonModel : JsonModel
{
    [JsonProperty("slot")]
    public int Slot { get; set; }
    
    [JsonProperty("team")]
    public string Team { get; set; } = null!;
    
    [JsonProperty("pass")]
    public bool Pass { get; set; }
}