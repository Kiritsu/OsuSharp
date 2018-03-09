using System;
using Newtonsoft.Json;

namespace OsuSharp.UserEndpoint
{
    public class Event
    {
        [JsonProperty("display_html")]
        public string DisplayHtml { get; set; }

        [JsonProperty("beatmap_id")]
        public ulong BeatmapId { get; set; }

        [JsonProperty("beatmapset_id")]
        public ulong BeatmapsetId { get; set; }

        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        [JsonProperty("epicfactor")]
        public ushort Epicfactor { get; set; }
    }
}