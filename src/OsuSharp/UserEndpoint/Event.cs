using System;
using Newtonsoft.Json;

namespace OsuSharp.UserEndpoint
{
    public class Event
    {
        /// <summary>
        ///     ???
        /// </summary>
        [JsonProperty("display_html")]
        public string DisplayHtml { get; set; }

        /// <summary>
        ///     Id of the beatmap
        /// </summary>
        [JsonProperty("beatmap_id")]
        public ulong BeatmapId { get; set; }

        /// <summary>
        ///     Id of the beatmapset
        /// </summary>
        [JsonProperty("beatmapset_id")]
        public ulong BeatmapsetId { get; set; }

        /// <summary>
        ///     Date of the event
        /// </summary>
        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        /// <summary>
        ///     ???
        /// </summary>
        [JsonProperty("epicfactor")]
        public ushort Epicfactor { get; set; }
    }
}