using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.Endpoints
{
    public class Multiplayer
    {
        /// <summary>
        ///     Represents the room
        /// </summary>
        [JsonProperty("match")]
        public Match Match { get; set; }

        /// <summary>
        ///     Represents every games played in this room
        /// </summary>
        [JsonProperty("games")]
        public List<Game> Games { get; set; }
    }
}