namespace OsuSharp.Domain
{
    public sealed class BeatmapAvailability
    {
        public bool DownloadDisabled { get; internal set; }
        
        public string MoreInformation { get; internal set; }
    }
}
