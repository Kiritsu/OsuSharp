using System.Collections.Generic;

namespace OsuSharp.Interfaces;

/// <summary>
/// Defines a beatmapset search enumeration result object.
/// </summary>
public interface IBeatmapsetSearchEnumeration
{
    /// <summary>
    /// Gets the beatmapsets that were pulled from the API.
    /// </summary>
    IReadOnlyList<IBeatmapset> Beatmapsets { get; }

    /// <summary>
    /// Gets the current cursor for pagination.
    /// </summary>
    ICursor Cursor { get; }

    /// <summary>
    /// Gets the object representing an error, if any.
    /// </summary>
    object Error { get; }

    /// <summary>
    /// Gets the recommended difficulty staring.
    /// </summary>
    double? RecommendedDifficulty { get; }

    /// <summary>
    /// Gets the search object.
    /// </summary>
    ISearch Search { get; }

    /// <summary>
    /// Gets the total amount of available beatmapsets.
    /// </summary>
    long Total { get; }
}