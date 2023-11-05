using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class MultiplayerMatchEventGameBeatmapJsonModel : JsonModel
{
    [JsonProperty("beatmapset_id")]
    public long BeatmapsetId { get; set; }
    
    [JsonProperty("difficulty_rating")]
    public double DifficultyRating { get; set; }
    
    [JsonProperty("id")]
    public long Id { get; set; }
    
    [JsonProperty("mode")]
    public string Mode { get; set; } = null!;
    
    [JsonProperty("status")]
    public string Status { get; set; } = null!;
    
    [JsonProperty("total_length")]
    public long TotalLength { get; set; }
    
    [JsonProperty("user_id")]
    public long UserId { get; set; }
    
    [JsonProperty("version")]
    public string Version { get; set; } = null!;
    
    [JsonProperty("beatmapset")]
    public MultiplayerMatchEventGameBeatmapsetJsonModel Beatmapset { get; set; } = null!;
}