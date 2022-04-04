using System;

namespace OsuSharp.Domain;

/// <summary>
/// Represents the general parameters of a beatmap search lookup.
/// </summary>
[Flags]
public enum BeatmapSearchGeneral
{
    /// <summary>
    /// No general parameters.
    /// </summary>
    None = 0,

    /// <summary>
    /// Include recommended beatmaps.
    /// </summary>
    Recommended = 1,

    /// <summary>
    /// Include converted beatmaps.
    /// </summary>
    Converts = 2,

    /// <summary>
    /// Include followed mappers' beatmaps.
    /// </summary>
    Follows = 4
}