using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OsuSharp.Endpoints;
using OsuSharp.Misc;

namespace OsuSharp.Endpoints
{
    public class Game
    {
        /// <summary>
        ///     Id of the game
        /// </summary>
        [JsonProperty("game_id")]
        public long GameId { get; set; }

        /// <summary>
        ///     Time where the game started
        /// </summary>
        [JsonProperty("start_time")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        ///     Time where the game ended
        /// </summary>
        [JsonProperty("end_time")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        ///     Id of the beatmap played
        /// </summary>
        [JsonProperty("beatmap_id")]
        public long BeatmapId { get; set; }

        /// <summary>
        ///     Game mode of the play
        /// </summary>
        [JsonProperty("play_mode")]
        public int GameMode { get; set; }

        /// <summary>
        ///     Match type
        /// </summary>
        [JsonProperty("match_type")]
        public int MatchType { get; set; }

        /// <summary>
        ///     Score type
        /// </summary>
        [JsonProperty("scoring_type")]
        public int ScoringType { get; set; }

        /// <summary>
        ///     Team type
        /// </summary>
        [JsonProperty("team_type")]
        public int TeamType { get; set; }

        /// <summary>
        ///     Mods used
        /// </summary>
        [JsonProperty("enabled_mods")]
        public int EnabledMods { get; set; }

        /// <summary>
        ///     Better representation of 'EnabledMods'
        /// </summary>
        public Mods Mods
            => (Mods)EnabledMods;

        /// <summary>
        ///     List of scores made by players in this game
        /// </summary>
        [JsonProperty("scores")]
        public List<ScoreMatch> Scores { get; set; }
    }
}