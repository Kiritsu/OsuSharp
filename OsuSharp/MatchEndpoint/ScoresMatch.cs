using System;
using Newtonsoft.Json;

namespace OsuSharp.MatchEndpoint
{
    public class ScoresMatch
    {
        [JsonProperty("pass")] private ushort _pass;

        [JsonProperty("perfect")] private ushort _perfect;

        [JsonProperty("slot")]
        public ushort Slot { get; set; }

        [JsonProperty("team")]
        public ushort Team { get; set; }

        [JsonProperty("user_id")]
        public ulong Userid { get; set; }

        [JsonProperty("score")]
        public uint Score { get; set; }

        [JsonProperty("maxcombo")]
        public uint? MaxCombo { get; set; }

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

        public bool Perfect
        {
            get { return Convert.ToBoolean(_perfect); }
            set { Perfect = value; }
        }

        public bool Pass
        {
            get { return Convert.ToBoolean(_pass); }
            set { Pass = value; }
        }
    }
}