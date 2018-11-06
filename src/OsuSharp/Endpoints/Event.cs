using System;
using Newtonsoft.Json;

namespace OsuSharp.Endpoints
{
    public sealed class Event
    {
        /// <summary>
        ///     Icon Url of the current event
        /// </summary>
        [JsonProperty("display_html")]
        public string DisplayHtml { get; internal set; }

        /// <summary>
        ///     Id of the beatmap
        /// </summary>
        [JsonProperty("beatmap_id", NullValueHandling = NullValueHandling.Ignore)]
        public long BeatmapId { get; internal set; }

        /// <summary>
        ///     Id of the beatmapset
        /// </summary>
        [JsonProperty("beatmapset_id", NullValueHandling = NullValueHandling.Ignore)]
        public long BeatmapsetId { get; internal set; }

        /// <summary>
        ///     Date of the event
        /// </summary>
        [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Date { get; internal set; }

        /// <summary>
        ///     Epicfactor of the event
        /// </summary>
        [JsonProperty("epicfactor", NullValueHandling = NullValueHandling.Ignore)]
        public int Epicfactor { get; internal set; }
    }
}