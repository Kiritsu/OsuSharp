using System;
using Newtonsoft.Json;

namespace OsuSharp.UserRecent
{
    public class UserRecent
    {
        [JsonProperty("perfect")] private ushort _perfect;

        [JsonProperty("beatmap_id")]
        public ulong BeatmapId { get; set; }

        [JsonProperty("score")]
        public ulong Score { get; set; }

        [JsonProperty("maxcombo")]
        public uint? MaxCombo { get; set; }

        [JsonProperty("count300")]
        public uint Count300 { get; set; }

        [JsonProperty("count100")]
        public uint Count100 { get; set; }

        [JsonProperty("count50")]
        public uint Count50 { get; set; }

        [JsonProperty("countmiss")]
        public uint Miss { get; set; }

        [JsonProperty("countkatu")]
        public uint Katu { get; set; }

        [JsonProperty("countgeki")]
        public uint Geki { get; set; }

        public bool Perfect
        {
            get { return Convert.ToBoolean(_perfect); }
            set { Perfect = value; }
        }

        [JsonProperty("enabled_mods")]
        public uint Mods { get; set; }

        [JsonProperty("user_id")]
        public ulong Userid { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("rank")]
        public string Rank { get; set; }
    }
}