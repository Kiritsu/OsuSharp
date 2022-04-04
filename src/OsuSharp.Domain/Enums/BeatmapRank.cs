using System;

namespace OsuSharp.Domain;

/// <summary>
/// Represents the possible ranks of a beatmap.
/// </summary>
[Flags]
public enum BeatmapRank
{
    /// <summary>
    /// No rank.
    /// </summary>
    None = 0,

    /// <summary>
    /// Rank Silver SS
    /// </summary>
    XH = 1,

    /// <summary>
    /// Rank SS
    /// </summary>
    X = 2,

    /// <summary>
    /// Rank Silver S
    /// </summary>
    SH = 4,

    /// <summary>
    /// Rank S
    /// </summary>
    S = 8,

    /// <summary>
    /// Rank A
    /// </summary>
    A = 16,

    /// <summary>
    /// Rank B
    /// </summary>
    B = 32,

    /// <summary>
    /// Rank C
    /// </summary>
    C = 64,

    /// <summary>
    /// Rank D
    /// </summary>
    D = 128
}