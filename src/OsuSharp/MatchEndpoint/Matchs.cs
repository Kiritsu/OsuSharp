using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.MatchEndpoint
{
    public class Matchs
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