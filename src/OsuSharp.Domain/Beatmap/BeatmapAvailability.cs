using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class BeatmapAvailability : IBeatmapAvailability
{
    public bool DownloadDisabled { get; internal set; }

    public string MoreInformation { get; internal set; } = null!;

    internal BeatmapAvailability()
    {
            
    }
}