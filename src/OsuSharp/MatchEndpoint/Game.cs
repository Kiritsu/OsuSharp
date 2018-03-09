using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OsuSharp.Misc;

namespace OsuSharp.MatchEndpoint
{
    public class Game
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
        public ushort GameMode { get; set; }

        [JsonProperty("match_type")]
        public ushort MatchType { get; set; }

        [JsonProperty("scoring_type")]
        public ushort ScoringType { get; set; }

        [JsonProperty("team_type")]
        public ushort TeamType { get; set; }

        [JsonProperty("enabled_mods")]
        public uint EnabledMods { get; set; }

        public Mods EnabledModsEnum
        {
            get { return (Mods) EnabledMods; }
        }

        public string Mods
        {
            get { return ((Mods) EnabledMods).ToModString(); }
        }

        [JsonProperty("scores")]
        public List<ScoreMatch> Scores { get; set; }
    }
}