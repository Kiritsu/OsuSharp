using System;
using Newtonsoft.Json;

namespace OsuSharp.Endpoints
{
    public class Match
    {
        /// <summary>
        ///     Id of the match
        /// </summary>
        [JsonProperty("match_id")]
        public long MatchId { get; set; }

        /// <summary>
        ///     Name of the room
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        ///     Time room was created
        /// </summary>
        [JsonProperty("start_time")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        ///     Time room got destroyed
        /// </summary>
        [JsonProperty("end_time")]
        public DateTime? EndTime { get; set; }
    }
}