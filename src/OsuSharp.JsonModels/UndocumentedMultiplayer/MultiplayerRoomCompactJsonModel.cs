using System;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class MultiplayerRoomCompactJsonModel : JsonModel
{
    /// <summary>
    ///     Gets the multiplayer match id.
    /// </summary>
    [JsonProperty("id")]
    public long MatchId { get; internal set; }

    /// <summary>
    ///     Gets the name of the multiplayer match.
    /// </summary>
    [JsonProperty("name")]
    public string Name { get; internal set; } = null!;

    /// <summary>
    ///     Gets the date time when the match created.
    /// </summary>
    [JsonProperty("start_time")]
    public DateTimeOffset StartTime { get; internal set; }

    /// <summary>
    ///     Gets the date time when the match ended.
    /// </summary>
    [JsonProperty("end_time")]
    public DateTimeOffset? EndTime { get; internal set; }
}