using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OsuSharp.MatchEndpoint
{
    public class Matchs
    {
        [JsonProperty("match")]
        public Match Match { get; set; }

        [JsonProperty("games")]
        public List<Games> Games { get; set; }
    }

    public class Match
    {
        [JsonProperty("match_id")]
        public ulong MatchId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("start_time")]
        public DateTime? StartTime { get; set; }

        [JsonProperty("end_time")]
        public DateTime? EndTime { get; set; }
    }

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

    public class ScoresMatch
    {
        [JsonProperty("slot")]
        public ushort Slot { get; set; }

        [JsonProperty("team")]
        public ushort Team { get; set; }

        [JsonProperty("user_id")]
        public ulong Userid { get; set; }

        [JsonProperty("score")]
        public uint Score { get; set; }

        [JsonProperty("maxcombo")]
        public uint MaxCombo { get; set; }

        [JsonProperty("rank")]
        public uint Rank { get; set; }

        [JsonProperty("count50")]
        public uint Count50 { get; set; }

        [JsonProperty("count100")]
        public uint Count100 { get; set; }

        [JsonProperty("count300")]
        public uint Count300 { get; set; }

        [JsonProperty("countmiss")]
        public uint Miss { get; set; }

        [JsonProperty("countgeki")]
        public uint Geki { get; set; }

        [JsonProperty("countkatu")]
        public uint Katu { get; set; }

        [JsonProperty("perfect")]
        private ushort _Perfect;

        public bool Perfect
        {
            get { return Convert.ToBoolean(_Perfect); }
            set { Perfect = value; }
        }

        [JsonProperty("pass")]
        private ushort _Pass;

        public bool Pass
        {
            get { return Convert.ToBoolean(_Pass); }
            set { Pass = value; }
        }
    }
}
