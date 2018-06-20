using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OsuSharp.Misc;

namespace OsuSharp.MatchEndpoint
{
    public class Game
    {
        /// <summary>
        /// Id of the game
        /// </summary>
        [JsonProperty("game_id")]
        public ulong GameId { get; set; }

        /// <summary>
        /// Time where the game started
        /// </summary>
        [JsonProperty("start_time")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Time where the game ended
        /// </summary>
        [JsonProperty("end_time")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// Id of the beatmap played
        /// </summary>
        [JsonProperty("beatmap_id")]
        public ulong BeatmapId { get; set; }

        /// <summary>
        /// Game mode of the play
        /// </summary>
        [JsonProperty("play_mode")]
        public ushort GameMode { get; set; }

        /// <summary>
        /// Match type
        /// </summary>
        [JsonProperty("match_type")]
        public ushort MatchType { get; set; }

        /// <summary>
        /// Score type
        /// </summary>
        [JsonProperty("scoring_type")]
        public ushort ScoringType { get; set; }

        /// <summary>
        /// Team type
        /// </summary>
        [JsonProperty("team_type")]
        public ushort TeamType { get; set; }

        /// <summary>
        /// Mods used
        /// </summary>
        [JsonProperty("enabled_mods")]
        public uint EnabledMods { get; set; }

        /// <summary>
        /// Better representation of 'EnabledMods'
        /// </summary>
        public Mods Mods
        {
            get { return (Mods) EnabledMods; }
        }

        /// <summary>
        /// List of scores made by players in this game
        /// </summary>
        [JsonProperty("scores")]
        public List<ScoreMatch> Scores { get; set; }
    }
}