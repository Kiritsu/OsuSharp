using Newtonsoft.Json;
using System.Collections.Generic;

namespace OsuSharp.Entities
{
    public sealed class MultiplayerRoom : EntityBase
    {
        internal MultiplayerRoom() { }

        /// <summary>
        ///     Gets a few informations about the current multiplayer room.
        /// </summary>
        [JsonProperty("match")]
        public MultiplayerMatch Match { get; internal set; }

        /// <summary>
        ///     Gets the different games played on this multiplayer room.
        /// </summary>
        [JsonProperty("games")]
        public IReadOnlyList<MultiplayerGame> Games { get; internal set; }
    }
}
