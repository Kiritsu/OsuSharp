using Newtonsoft.Json;
using System;

namespace OsuSharp.BeatmapsEndpoint
{
    public class Beatmaps
    {
        [JsonProperty("beatmapset_id")]
        public ulong BeatmapsetID { get; set; }

        [JsonProperty("beatmap_id")]
        public ulong BeatmapID { get; set; }

        [JsonProperty("approved")]
        private string _Approved;

        public string Approved
        {
            get { return Converter.Approved.ApprovedConverter(_Approved); }
            set { _Approved = value; }
        }

        [JsonProperty("total_length")]
        public ushort TotalLength { get; set; }

        [JsonProperty("hit_length")]
        public ushort HitLength { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("file_md5")]
        public string FileMD5 { get; set; }

        [JsonProperty("diff_size")]
        public float CS { get; set; }

        [JsonProperty("diff_overall")]
        public float OD { get; set; }

        [JsonProperty("diff_approach")]
        public float AR { get; set; }

        [JsonProperty("diff_drain")]
        public float HP { get; set; }

        [JsonProperty("mode")]
        public ushort Mode { get; set; }

        [JsonProperty("approved_date")]
        public DateTime? ApprovedDate { get; set; }

        [JsonProperty("last_update")]
        public DateTime? LastUpdate { get; set; }

        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }

        [JsonProperty("bpm")]
        public float BPM { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("genre_id")]
        public ushort GenreId { get; set; }

        [JsonProperty("language_id")]
        public ushort LanguageId { get; set; }

        [JsonProperty("favourite_count")]
        public ushort FavouriteCount { get; set; }

        [JsonProperty("playcount")]
        public uint PlayCount { get; set; }

        [JsonProperty("passcount")]
        public uint PassCount { get; set; }

        [JsonProperty("max_combo")]
        public uint MaxCombo { get; set; }

        [JsonProperty("difficultyrating")]
        public float DifficultyRating { get; set; }
    }
}
