using System;
using Newtonsoft.Json;

namespace OsuSharp.Legacy.Entities;

public sealed class LegacyMultiplayerMatch : LegacyEntityBase
{
    internal LegacyMultiplayerMatch() { }

    /// <summary>
    ///     Gets the multiplayer match id.
    /// </summary>
    [JsonProperty("match_id")]
    public long MatchId { get; internal set; }

    /// <summary>
    ///     Gets the name of the multiplayer match.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; internal set; }

    /// <summary>
    ///     Gets the date time when the match started.
    /// </summary>
    [JsonProperty("start_time")]
    public DateTimeOffset? StartTime { get; internal set; }

    /// <summary>
    ///     Gets the date time when the match ended.
    /// </summary>
    [JsonProperty("end_time")]
    public DateTimeOffset? EndTime { get; internal set; }
}