using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class BeatmapHypeJsonModel
    {
        [JsonProperty("current")]
        public int CurrentHype { get; internal set; }

        [JsonProperty("required")]
        public int RequiredHype { get; internal set; }

        internal BeatmapHypeJsonModel()
        {
        }
    }
}