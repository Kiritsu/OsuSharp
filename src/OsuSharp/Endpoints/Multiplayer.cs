using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.Endpoints
{
    public sealed class Multiplayer
    {
        /// <summary>
        ///     Represents the room
        /// </summary>
        [JsonProperty("match")]
        public Match Match { get; internal set; }

        [JsonProperty("games")]
        internal List<Game> _games;

        /// <summary>
        ///     Represents every games played in this room
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<Game> Games
            => _games.AsReadOnly();
    }
}