using System;
using System.Collections.Generic;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public class MultiplayerMatchEventGameScore : IMultiplayerMatchEventGameScore
{
    public double Accuracy { get; set; }
    public long? BestId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public long? Id { get; set; }
    public int MaxCombo { get; set; }
    public GameMode Mode { get; set; }
    public int ModeInt { get; set; }
    public IReadOnlyList<string> Mods { get; set; } = null!;
    public bool Passed { get; set; }
    public bool Perfect { get; internal set; } 
    public double? Pp { get; set; }
    public string Rank { get; set; } = null!;
    public long Score { get; set; }
    public IStatistics Statistics { get; set; } = null!;
    public string Type { get; set; } = null!; //TODO: add score type (legacy_match_score)
    public long UserId { get; set; }
    public IMultiplayerMatchEventGameSlotInfo SlotInfo { get; set; } = null!;

    internal MultiplayerMatchEventGameScore()
    {
        
    }
}