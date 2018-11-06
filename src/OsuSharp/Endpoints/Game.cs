using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OsuSharp.Misc;

namespace OsuSharp.Endpoints
{
    public sealed class Game
    {
        /// <summary>
        ///     Id of the game
        /// </summary>
        [JsonProperty("game_id")]
        public long GameId { get; internal set; }

        /// <summary>
        ///     Time where the game started
        /// </summary>
        [JsonProperty("start_time", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime StartTime { get; internal set; }

        /// <summary>
        ///     Time where the game ended
        /// </summary>
        [JsonProperty("end_time", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime EndTime { get; internal set; }

        /// <summary>
        ///     Id of the beatmap played
        /// </summary>
        [JsonProperty("beatmap_id")]
        public long BeatmapId { get; internal set; }

        /// <summary>
        ///     Game mode of the play
        /// </summary>
        [JsonProperty("play_mode")]
        public int GameMode { get; internal set; }

        /// <summary>
        ///     Match type
        /// </summary>
        [JsonProperty("match_type")]
        public int MatchType { get; internal set; }

        /// <summary>
        ///     Score type
        /// </summary>
        [JsonProperty("scoring_type")]
        public int ScoringType { get; internal set; }

        /// <summary>
        ///     Team type
        /// </summary>
        [JsonProperty("team_type")]
        public int TeamType { get; internal set; }

        /// <summary>
        ///     Mods used
        /// </summary>
        [JsonProperty("enabled_mods")]
        public int EnabledMods { get; internal set; }

        /// <summary>
        ///     Better representation of 'EnabledMods'
        /// </summary>
        [JsonIgnore]
        public Mods Mods
            => (Mods)EnabledMods;

        [JsonProperty("scores")]
        internal List<ScoreMatch> _scores;

        /// <summary>
        ///     List of scores made by players in this game
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<ScoreMatch> Scores
            => _scores.AsReadOnly();
    }
}