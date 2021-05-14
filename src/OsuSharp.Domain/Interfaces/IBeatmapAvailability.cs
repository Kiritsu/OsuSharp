namespace OsuSharp.Interfaces
{
    public interface IBeatmapAvailability
    {
        bool DownloadDisabled { get; }
        string MoreInformation { get; }
    }
}