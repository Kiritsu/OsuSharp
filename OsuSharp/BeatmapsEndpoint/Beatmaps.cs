using System;
using Newtonsoft.Json;

namespace OsuSharp.BeatmapsEndpoint
{
    public class Beatmaps
    {
        [JsonProperty("approved")] private string _approved;

        [JsonProperty("beatmapset_id")]
        public ulong BeatmapsetId { get; set; }

        [JsonProperty("beatmap_id")]
        public ulong BeatmapId { get; set; }

        public string Approved
        {
            get { return Converter.Approved.ApprovedConverter(_approved); }
            set { _approved = value; }
        }

        [JsonProperty("total_length")]
        public ushort TotalLength { get; set; }

        [JsonProperty("hit_length")]
        public ushort HitLength { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("file_md5")]
        public string FileMd5 { get; set; }

        [JsonProperty("diff_size")]
        public float Cs { get; set; }

        [JsonProperty("diff_overall")]
        public float Od { get; set; }

        [JsonProperty("diff_approach")]
        public float Ar { get; set; }

        [JsonProperty("diff_drain")]
        public float Hp { get; set; }

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
        public float Bpm { get; set; }

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
        public uint? MaxCombo { get; set; }

        [JsonProperty("difficultyrating")]
        public float DifficultyRating { get; set; }
    }
}