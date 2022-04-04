using System;

namespace OsuSharp.Domain;

/// <summary>
/// Represents the extra parameters of a beatmap search lookup.
/// </summary>
[Flags]
public enum BeatmapSearchExtra
{
    /// <summary>
    /// No extra parameters
    /// </summary>
    None = 0,

    /// <summary>
    /// Include videos.
    /// </summary>
    Video = 1,

    /// <summary>
    /// Include storyboards.
    /// </summary>
    Storyboard = 2
}