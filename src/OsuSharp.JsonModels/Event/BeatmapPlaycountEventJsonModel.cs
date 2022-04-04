using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class BeatmapPlaycountEventJsonModel : EventJsonModel
{
    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("beatmap")]
    public EventBeatmapJsonModel Beatmap { get; set; } = null!;
}