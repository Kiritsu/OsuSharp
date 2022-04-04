using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.Legacy.Entities;

public sealed class LegacyMultiplayerRoom : LegacyEntityBase
{
    internal LegacyMultiplayerRoom() { }

    /// <summary>
    ///     Information about the current match
    /// </summary>
    [JsonProperty("match")]
    public LegacyMultiplayerMatch Match { get; internal set; }

    /// <summary>
    ///     Games played on this multiplayer room.
    /// </summary>
    [JsonProperty("games")]
    public IReadOnlyList<LegacyMultiplayerGame> Games { get; internal set; }
}