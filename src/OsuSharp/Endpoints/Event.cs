using System;
using Newtonsoft.Json;

namespace OsuSharp.Endpoints
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
        public long BeatmapId { get; set; }

        /// <summary>
        ///     Id of the beatmapset
        /// </summary>
        [JsonProperty("beatmapset_id")]
        public long BeatmapsetId { get; set; }

        /// <summary>
        ///     Date of the event
        /// </summary>
        [JsonProperty("date")]
        public DateTime? Date { get; set; }

        /// <summary>
        ///     ???
        /// </summary>
        [JsonProperty("epicfactor")]
        public int Epicfactor { get; set; }
    }
}