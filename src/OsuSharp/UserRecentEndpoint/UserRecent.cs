using System;
using Newtonsoft.Json;
using OsuSharp.Misc;

namespace OsuSharp.UserRecentEndpoint
{
    public class UserRecent
    {
        [JsonProperty("perfect")]
        private ushort _perfect;

        [JsonProperty("beatmap_id")]
        public ulong BeatmapId { get; set; }

        [JsonProperty("score")]
        public long ScorePoints { get; set; }

        [JsonProperty("maxcombo")]
        public int? MaxCombo { get; set; }

        [JsonProperty("count300")]
        public int Count300 { get; set; }

        [JsonProperty("count100")]
        public int Count100 { get; set; }

        [JsonProperty("count50")]
        public int Count50 { get; set; }

        [JsonIgnore]
        public double Accuracy
        {
            get
            {
                return (Count50 * 50 + Count100 * 100 + Count300 * 300)
                       / (300.0 * (Count50 + Count100 + Count300 + Miss)) * 100;
            }
        }

        [JsonProperty("countmiss")]
        public int Miss { get; set; }

        [JsonProperty("countkatu")]
        public int Katu { get; set; }

        [JsonProperty("countgeki")]
        public int Geki { get; set; }

        public bool Perfect
        {
            get { return Convert.ToBoolean(_perfect); }
        }

        [JsonProperty("enabled_mods")]
        public int EnabledMods { get; set; }

        public Mods Mods
        {
            get { return (Mods)EnabledMods; }
        }

        [JsonProperty("user_id")]
        public ulong Userid { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("rank")]
        public string Rank { get; set; }
    }
}