using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class BeatmapAvailabilityJsonModel
    {
        [JsonProperty("download_disabled")]
        public bool DownloadDisabled { get; internal set; }

        [JsonProperty("more_information")]
        public string MoreInformation { get; internal set; }

        internal BeatmapAvailabilityJsonModel()
        {
        }
    }
}