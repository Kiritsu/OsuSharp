using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class BeatmapAvailabilityJsonModel : JsonModel
{
    [JsonProperty("download_disabled")]
    public bool DownloadDisabled { get; set; }

    [JsonProperty("more_information")]
    public string MoreInformation { get; set; } = null!;
}