using System;
using Newtonsoft.Json;

namespace OsuSharp.Legacy.Entities;

public sealed class LegacyUserEvents : LegacyEntityBase
{
    internal LegacyUserEvents() { }

    /// <summary>
    ///     Gets the HTML display for that event.
    /// </summary>
    [JsonProperty("display_html")]
    public string DisplayHtml { get; internal set; }

    /// <summary>
    ///     Id of the beatmap associated with that event.
    /// </summary>
    [JsonProperty("beatmap_id")]
    public long? BeatmapId { get; internal set; }

    /// <summary>
    ///     Id of the beatmapset associated with that event.
    /// </summary>
    [JsonProperty("beatmapset_id")]
    public long? BeatmapsetId { get; internal set; }

    /// <summary>
    ///     Gets the date the event was.
    /// </summary>
    [JsonProperty("date")]
    public DateTimeOffset? EventDate { get; internal set; }

    /// <summary>
    ///     Gets the epicness of this event. [1;32]
    /// </summary>
    [JsonProperty("epicfactor")]
    public int? EpicFactor { get; internal set; }
}