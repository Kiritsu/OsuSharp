using System.Runtime.Serialization;

namespace OsuSharp.Domain;

public enum TeamType
{
    [EnumMember(Value = "Head-To-Head")]
    HeadToHead,
    
    [EnumMember(Value = "Team-Vs-Team")]
    TeamVsTeam
}