using System;
using System.Collections.Generic;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface IMultiplayerMatchEventGameScore
{
    double Accuracy { get; }

    /// <summary>
    ///     The best score ID. Pretty much never populated.
    /// </summary>
    long? BestId { get; }

    DateTimeOffset CreatedAt { get; }

    /// <summary>
    ///     The score ID. Most of the time isn't populated.
    /// </summary>
    long? Id { get; }

    int MaxCombo { get; }
    GameMode Mode { get; }
    int ModeInt { get; }
    IReadOnlyList<string> Mods { get; }
    bool Passed { get; }
    bool Perfect { get; }

    /// <summary>
    ///     Resulting pps from the score. Most of the times is null. 
    /// </summary>
    double? Pp { get; }

    string Rank { get; }
    long Score { get; }
    IStatistics Statistics { get; }
    ScoreType Type { get; }
    long UserId { get; }
    IMultiplayerMatchEventGameSlotInfo SlotInfo { get; }
}