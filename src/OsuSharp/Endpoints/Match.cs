using System;
using Newtonsoft.Json;

namespace OsuSharp.Endpoints
{
    public sealed class Match
    {
        /// <summary>
        ///     Id of the match
        /// </summary>
        [JsonProperty("match_id")]
        public long MatchId { get; internal set; }

        /// <summary>
        ///     Name of the room
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; internal set; }

        /// <summary>
        ///     Time room was created
        /// </summary>
        [JsonProperty("start_time", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime StartTime { get; internal set; }

        /// <summary>
        ///     Time room got destroyed
        /// </summary>
        [JsonProperty("end_time", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime EndTime { get; internal set; }

        internal Match()
        {

        }
    }
}