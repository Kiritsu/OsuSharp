using System;
using Newtonsoft.Json;

namespace OsuSharp.MatchEndpoint
{
    public class ScoreMatch
    {
        [JsonProperty("pass")]
        private short _pass;

        [JsonProperty("perfect")]
        private short _perfect;

        [JsonProperty("slot")]
        public ushort SlotId { get; set; }

        [JsonProperty("team")]
        public ushort TeamId { get; set; }

        [JsonProperty("user_id")]
        public ulong Userid { get; set; }

        [JsonProperty("score")]
        public long ScorePoints { get; set; }

        [JsonProperty("maxcombo")]
        public int? MaxCombo { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("count50")]
        public int Count50 { get; set; }

        [JsonProperty("count100")]
        public int Count100 { get; set; }

        [JsonProperty("count300")]
        public int Count300 { get; set; }

        [JsonProperty("countmiss")]
        public int Miss { get; set; }

        [JsonProperty("countgeki")]
        public int Geki { get; set; }

        [JsonProperty("countkatu")]
        public int Katu { get; set; }

        public bool Perfect
        {
            get { return Convert.ToBoolean(_perfect); }
        }

        public bool Pass
        {
            get { return Convert.ToBoolean(_pass); }
        }

        [JsonIgnore]
        public double Accuracy
        {
            get { return (Count50 * 50 + Count100 * 100 + Count300 * 300) / (300.0 * (Count50 + Count100 + Count300 + Miss)) * 100; }
        }
    }
}