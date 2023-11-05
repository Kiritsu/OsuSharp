using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class MultiplayerMatchEventGameScoreJsonModel : JsonModel
{
    [JsonProperty("accuracy")]
    public double Accuracy { get; set; }
    
    /// <summary>
    ///     The best score ID. Pretty much never populated.
    /// </summary>
    [JsonProperty("best_id")]
    public long? BestId { get; set; }

    [JsonProperty("created_at")]
    public DateTimeOffset CreatedAt { get; set; }
    
    /// <summary>
    ///     The score ID. Most of the time isn't populated.
    /// </summary>
    [JsonProperty("id")] 
    public long? Id { get; set; }

    [JsonProperty("max_combo")]
    public int MaxCombo { get; set; }

    [JsonProperty("mode")] 
    public string Mode { get; set; } = null!;

    [JsonProperty("mode_int")] 
    public int ModeInt { get; set; }

    [JsonProperty("mods")] 
    public List<string> Mods { get; set; } = null!;

    [JsonProperty("passed")]
    public bool Passed { get; set; }
    
    [JsonIgnore]
    public bool Perfect => _perfect == 1;

    [JsonProperty("perfect")]
    private readonly int _perfect;
    
    /// <summary>
    ///     Resulting pps from the score. Most of the times is null. 
    /// </summary>
    [JsonProperty("pp")]
    public double? Pp { get; set; }

    [JsonProperty("rank")] 
    public string Rank { get; set; } = null!;
    
    [JsonProperty("replay")]
    public bool Replay { get; set; }

    [JsonProperty("score")]
    public long Score { get; set; }

    [JsonProperty("statistics")] 
    public StatisticsJsonModel Statistics { get; set; } = null!;

    [JsonProperty("type")]
    public string Type { get; set; } = null!;

    [JsonProperty("user_id")]
    public long UserId { get; set; }
    
    [JsonProperty("current_user_attributes")]
    public object CurrentUserAttributes { get; set; } // Concrete type is not required; This field is always {"pin": null}
    
    [JsonProperty("match")] 
    public MultiplayerMatchEventGameSlotInfoJsonModel SlotInfo { get; set; } = null!;
}