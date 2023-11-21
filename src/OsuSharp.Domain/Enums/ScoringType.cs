using System.Runtime.Serialization;

namespace OsuSharp.Domain;

public enum ScoringType
{
    [EnumMember(Value = "Score")]
    Score,
    
    [EnumMember(Value = "Max_Combo")]
    MaxCombo,
    
    [EnumMember(Value = "Accuracy")]
    Accuracy,
    
    [EnumMember(Value = "Scorev2")]
    Scorev2,
}