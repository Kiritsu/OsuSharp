using System;
using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public sealed class UserEvents : EntityBase
    {
        internal UserEvents() { }

        /// <summary>
        ///     Gets the HTML display for that event.
        /// </summary>
        [JsonProperty("display_html", NullValueHandling = NullValueHandling.Ignore)]
        public string DisplayHtml { get; internal set; }

        /// <summary>
        ///     Id of the beatmap associated with that event.
        /// </summary>
        [JsonProperty("beatmap_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? BeatmapId { get; internal set; }

        /// <summary>
        ///     Id of the beatmapset associated with that event.
        /// </summary>
        [JsonProperty("beatmapset_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? BeatmapsetId { get; internal set; }

        /// <summary>
        ///     Gets the date the event was.
        /// </summary>
        [JsonProperty("date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset? EventDate { get; internal set; }

        /// <summary>
        ///     Gets the epicness of this event. [1;32]
        /// </summary>
        [JsonProperty("epicfactor", NullValueHandling = NullValueHandling.Ignore)]
        public int? EpicFactor { get; internal set; }
    }
}
