#pragma warning disable 0649
using System;
using Newtonsoft.Json;
using OsuSharp.Misc;

namespace OsuSharp.ScoreEndpoint
{
    public class Score
    {
        [JsonProperty("perfect")]
        private ushort _perfect;

        /// <summary>
        /// Score points of the play
        /// </summary>
        [JsonProperty("score")]
        public ulong ScorePoints { get; set; }

        /// <summary>
        /// Score id
        /// </summary>
        [JsonProperty("score_id")]
        public ulong ScoreId { get; set; }

        /// <summary>
        /// Name of the player
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        /// <summary>
        /// Count of 300
        /// </summary>
        [JsonProperty("count300")]
        public int Count300 { get; set; }

        /// <summary>
        /// Count of 100
        /// </summary>
        [JsonProperty("count100")]
        public int Count100 { get; set; }

        /// <summary>
        /// Count of 50
        /// </summary>
        [JsonProperty("count50")]
        public int Count50 { get; set; }

        /// <summary>
        /// Count of misses
        /// </summary>
        [JsonProperty("countmiss")]
        public int Miss { get; set; }

        /// <summary>
        /// Accuracy of the play
        /// </summary>
        [JsonIgnore]
        public double Accuracy
        {
            get
            {
                return (Count50 * 50 + Count100 * 100 + Count300 * 300) 
                    / (300.0 * (Count50 + Count100 + Count300 + Miss)) * 100; 
            }
        }

        /// <summary>
        /// Max combo of the play
        /// </summary>
        [JsonProperty("maxcombo")]
        public int? MaxCombo { get; set; }

        /// <summary>
        /// Count of katus
        /// </summary>
        [JsonProperty("countkatu")]
        public int Katu { get; set; }

        /// <summary>
        /// Count of gekies
        /// </summary>
        [JsonProperty("countgeki")]
        public int Geki { get; set; }

        /// <summary>
        /// Is the map a perfect?
        /// </summary>
        public bool Perfect
        {
            get { return Convert.ToBoolean(_perfect); }
        }

        /// <summary>
        /// Mods used for this play
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
        /// Id of the player
        /// </summary>
        [JsonProperty("user_id")]
        public ulong Userid { get; set; }

        /// <summary>
        /// Date the score was submitted
        /// </summary>
        [JsonProperty("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Rank of the player (in the map leaderboard) for this play
        /// </summary>
        [JsonProperty("rank")]
        public string Rank { get; set; }

        /// <summary>
        /// Pp given by the map
        /// </summary>
        [JsonProperty("pp")]
        public float Pp { get; set; }
    }
}