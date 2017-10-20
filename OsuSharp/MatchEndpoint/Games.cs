using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.MatchEndpoint
{
    public class Games
    {
        [JsonProperty("game_id")]
        public ulong GameId { get; set; }

        [JsonProperty("start_time")]
        public DateTime? StartTime { get; set; }

        [JsonProperty("end_time")]
        public DateTime? EndTime { get; set; }

        [JsonProperty("beatmap_id")]
        public ulong BeatmapId { get; set; }

        [JsonProperty("play_mode")]
        public ushort PlayMode { get; set; }

        [JsonProperty("match_type")]
        public ushort MatchType { get; set; }

        [JsonProperty("scoring_type")]
        public ushort ScoringType { get; set; }

        [JsonProperty("team_type")]
        public ushort TeamType { get; set; }

        [JsonProperty("mods")]
        public uint Mods { get; set; }

        [JsonProperty("scores")]
        public List<ScoresMatch> Scores { get; set; }
    }
}