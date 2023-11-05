using System;
using System.Collections.Generic;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface IMultiplayerMatchEventGameScore
{
    double Accuracy { get; set; }

    /// <summary>
    ///     The best score ID. Pretty much never populated.
    /// </summary>
    long? BestId { get; set; }

    DateTimeOffset CreatedAt { get; set; }

    /// <summary>
    ///     The score ID. Most of the time isn't populated.
    /// </summary>
    long? Id { get; set; }

    int MaxCombo { get; set; }
    GameMode Mode { get; set; }
    int ModeInt { get; set; }
    IReadOnlyList<string> Mods { get; set; }
    bool Passed { get; set; }
    bool Perfect { get; }

    /// <summary>
    ///     Resulting pps from the score. Most of the times is null. 
    /// </summary>
    double? Pp { get; set; }

    string Rank { get; set; }
    long Score { get; set; }
    IStatistics Statistics { get; set; }
    string Type { get; set; }
    long UserId { get; set; }
    IMultiplayerMatchEventGameSlotInfo SlotInfo { get; set; }
}