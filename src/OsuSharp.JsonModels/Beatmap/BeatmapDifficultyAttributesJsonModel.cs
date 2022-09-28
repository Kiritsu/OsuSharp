using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class BeatmapDifficultyAttributesJsonModel : JsonModel
{
    [JsonProperty("max_combo")]
    public int MaxCombo { get; set; }
    
    [JsonProperty("star_rating")]
    public double StarRating { get; set; }
    
    [JsonProperty("aim_difficulty")]
    public double? AimDifficulty { get; set; }
    
    [JsonProperty("approach_rate")]
    public double? ApproachRate { get; set; }
    
    [JsonProperty("flashlight_difficulty")]
    public double? FlashlightDifficulty { get; set; }
    
    [JsonProperty("overall_difficulty")]
    public double? OverallDifficulty { get; set; }
    
    [JsonProperty("slider_factor")]
    public double? SliderFactor { get; set; }
    
    [JsonProperty("speed_difficulty")]
    public double? SpeedDifficulty { get; set; }
    
    [JsonProperty("stamina_difficulty")]
    public double? StaminaDifficulty { get; set; }
    
    [JsonProperty("rhythm_difficulty")]
    public double? RhythmDifficulty { get; set; }
    
    [JsonProperty("colour_difficulty")]
    public double? ColourDifficulty { get; set; }
    
    [JsonProperty("great_hit_window")]
    public double? GreatHitWindow { get; set; }
    
    [JsonProperty("score_multiplier")]
    public double? ScoreMultiplier { get; set; }
}