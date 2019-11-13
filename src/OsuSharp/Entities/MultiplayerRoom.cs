using Newtonsoft.Json;
using System.Collections.Generic;

namespace OsuSharp
{
    public sealed class MultiplayerRoom : EntityBase
    {
        internal MultiplayerRoom() { }

        /// <summary>
        ///     Information about the current match
        /// </summary>
        [JsonProperty("match")]
        public MultiplayerMatch Match { get; internal set; }

        /// <summary>
        ///     Games played on this multiplayer room.
        /// </summary>
        [JsonProperty("games")]
        public IReadOnlyList<MultiplayerGame> Games { get; internal set; }
    }
}
