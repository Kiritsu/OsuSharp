namespace OsuSharp.Interfaces;

/// <summary>
/// Defines a beatmap availability object.
/// </summary>
public interface IBeatmapAvailability
{
    /// <summary>
    /// Gets whether the download is disabled or not.
    /// </summary>
    bool DownloadDisabled { get; }

    /// <summary>
    /// Gets the optional information about the availability of the beatmap.
    /// </summary>
    string MoreInformation { get; }
}