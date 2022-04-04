namespace OsuSharp.Interfaces;

/// <summary>
/// Defines the hype of a beatmap.
/// </summary>
public interface IBeatmapHype
{
    /// <summary>
    /// Gets the current hype of a beatmap.
    /// </summary>
    int CurrentHype { get; }

    /// <summary>
    /// Gets the required hype of a beatmap.
    /// </summary>
    int RequiredHype { get; }
}