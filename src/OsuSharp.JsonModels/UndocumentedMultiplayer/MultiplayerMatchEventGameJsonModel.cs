using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class MultiplayerMatchEventGameJsonModel : JsonModel
{
    [JsonProperty("beatmap_id")]
    public long? BeatmapId { get; set; }
    
    [JsonProperty("id")]
    public long Id { get; set; }
    
    [JsonProperty("start_time")]
    public DateTimeOffset StartTime { get; set; }

    [JsonProperty("end_time")]
    public DateTimeOffset? EndTime { get; set; }
    
    [JsonProperty("mode")]
    public string Mode { get; set; } = null!;
    
    [JsonProperty("mode_int")]
    public int ModeInt { get; set; }

    [JsonProperty("scoring_type")] 
    public string ScoringType { get; set; } = null!;

    [JsonProperty("team_type")] 
    public string TeamType { get; set; } = null!;

    [JsonProperty("mods")] 
    public List<string> Mods { get; set; } = null!;
    
    [JsonProperty("beatmap")] 
    public MultiplayerMatchEventGameBeatmapJsonModel? Beatmap { get; set; }

    [JsonProperty("scores")] 
    public List<MultiplayerMatchEventGameScoreJsonModel> Scores { get; set; } = null!;
}