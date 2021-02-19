using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class BeatmapAvailability
    {
        [JsonProperty("download_disabled")]
        public bool DownloadDisabled { get; internal set; }
        
        [JsonProperty("more_information")]
        public string MoreInformation { get; internal set; }

        internal BeatmapAvailability()
        {
            
        }
    }
}