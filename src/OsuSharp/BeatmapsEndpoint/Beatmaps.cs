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
            get { return Misc.Approved.ToString(_approved); }
            set { _approved = value; }
        }

        [JsonProperty("total_length")]
        public int TotalLength { get; set; }

        [JsonProperty("hit_length")]
        public int HitLength { get; set; }

        [JsonProperty("version")]
        public string Difficulty { get; set; }

        [JsonProperty("file_md5")]
        public string FileMd5 { get; set; }

        [JsonProperty("diff_size")]
        public float CircleSize { get; set; }

        [JsonProperty("diff_overall")]
        public float OverallDifficulty { get; set; }

        [JsonProperty("diff_approach")]
        public float ApproachRate { get; set; }

        [JsonProperty("diff_drain")]
        public float HpDrain { get; set; }

        [JsonProperty("mode")]
        public ushort GameMode { get; set; }

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
        public string Sources { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("genre_id")]
        public ushort GenreId { get; set; }

        [JsonProperty("language_id")]
        public ushort LanguageId { get; set; }

        [JsonProperty("favourite_count")]
        public int FavouriteCount { get; set; }

        [JsonProperty("playcount")]
        public int PlayCount { get; set; }

        [JsonProperty("passcount")]
        public int PassCount { get; set; }

        [JsonProperty("max_combo")]
        public int? MaxCombo { get; set; }

        [JsonProperty("difficultyrating")]
        public float DifficultyRating { get; set; }

        [JsonIgnore]
        public string ThumbnailUrl
        {
            get { return $"https://b.ppy.sh/thumb/{BeatmapsetId}l.jpg"; }
        }

        [JsonIgnore]
        public string CoverUrl
        {
            get { return $"https://assets.ppy.sh/beatmaps/{BeatmapsetId}/covers/cover.jpg"; }
        }

        [JsonIgnore]
        public string SoundPreviewUrl
        {
            get { return $"https://b.ppy.sh/preview/{BeatmapsetId}.mp3"; }
        }
    }
}