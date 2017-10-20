using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.MatchEndpoint
{
    public class Matchs
    {
        [JsonProperty("match")]
        public Match Match { get; set; }

        [JsonProperty("games")]
        public List<Games> Games { get; set; }
    }
}