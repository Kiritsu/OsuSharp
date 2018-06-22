#pragma warning disable 0649
using System;
using Newtonsoft.Json;
using OsuSharp.Misc;

namespace OsuSharp.BeatmapEndpoint
{
    public class Beatmap
    {
        [JsonProperty("approved")]
        private string _approved;

        /// <summary>
        /// Id of the beatmapset
        /// </summary>
        [JsonProperty("beatmapset_id")]
        public ulong BeatmapsetId { get; set; }

        /// <summary>
        /// Id of the beatmap
        /// </summary>
        [JsonProperty("beatmap_id")]
        public ulong BeatmapId { get; set; }

        /// <summary>
        /// Enum that represent the state of the map
        /// </summary>
        public BeatmapState Approved
        {
            get { return Misc.Approved.ToBeatmapState(_approved); }
        }

        /// <summary>
        /// Length of the beatmap (includes breaks)
        /// </summary>
        [JsonProperty("total_length")]
        public int TotalLength { get; set; }

        /// <summary>
        /// Length of the beatmap (doesn't include breaks)
        /// </summary>
        [JsonProperty("hit_length")]
        public int HitLength { get; set; }

        /// <summary>
        /// Difficulty of the beatmap
        /// </summary>
        [JsonProperty("version")]
        public string Difficulty { get; set; }

        [JsonProperty("file_md5")]
        public string FileMd5 { get; set; }

        /// <summary>
        /// Circle size (CS)
        /// </summary>
        [JsonProperty("diff_size")]
        public float CircleSize { get; set; }

        /// <summary>
        /// Overall difficulty (OD)
        /// </summary>
        [JsonProperty("diff_overall")]
        public float OverallDifficulty { get; set; }

        /// <summary>
        /// Approach rate (AR)
        /// </summary>
        [JsonProperty("diff_approach")]
        public float ApproachRate { get; set; }

        /// <summary>
        /// Hp drain (HP)
        /// </summary>
        [JsonProperty("diff_drain")]
        public float HpDrain { get; set; }

        /// <summary>
        /// Game mode of the map (standard/taiko/catch/mania)
        /// </summary>
        [JsonProperty("mode")]
        public ushort GameMode { get; set; }

        /// <summary>
        /// DateTime of beatmap's approval
        /// </summary>
        [JsonProperty("approved_date")]
        public DateTime? ApprovedDate { get; set; }

        /// <summary>
        /// DateTime of beatmap's latest update
        /// </summary>
        [JsonProperty("last_update")]
        public DateTime? LastUpdate { get; set; }

        /// <summary>
        /// Name of the artist
        /// </summary>
        [JsonProperty("artist")]
        public string Artist { get; set; }

        /// <summary>
        /// Title of the beatmap
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Name of the creator
        /// </summary>
        [JsonProperty("creator")]
        public string Creator { get; set; }

        /// <summary>
        /// BPM of the beatmap
        /// </summary>
        [JsonProperty("bpm")]
        public float Bpm { get; set; }

        /// <summary>
        /// Sources (added manually in bm editor)
        /// </summary>
        [JsonProperty("source")]
        public string Sources { get; set; }

        /// <summary>
        /// Tags (added manually in bm editor)
        /// </summary>
        [JsonProperty("tags")]
        public string Tags { get; set; }

        /// <summary>
        /// Genre id
        /// </summary>
        [JsonProperty("genre_id")]
        public ushort GenreId { get; set; }

        /// <summary>
        /// Language id
        /// </summary>
        [JsonProperty("language_id")]
        public ushort LanguageId { get; set; }

        /// <summary>
        /// Total count of favorites
        /// </summary>
        [JsonProperty("favourite_count")]
        public int FavouriteCount { get; set; }

        /// <summary>
        /// Total count of playcounts
        /// </summary>
        [JsonProperty("playcount")]
        public int PlayCount { get; set; }

        /// <summary>
        /// Total count of passcounts
        /// </summary>
        [JsonProperty("passcount")]
        public int PassCount { get; set; }

        /// <summary>
        /// Probably the count of combo max (may be null, depending on the 'GameMode'
        /// </summary>
        [JsonProperty("max_combo")]
        public int? MaxCombo { get; set; }

        /// <summary>
        /// Rating of the difficulty
        /// </summary>
        [JsonProperty("difficultyrating")]
        public float DifficultyRating { get; set; }

        /// <summary>
        /// Thumbnail url of the beatmap
        /// </summary>
        [JsonIgnore]
        public string ThumbnailUrl
        {
            get { return $"https://b.ppy.sh/thumb/{BeatmapsetId}l.jpg"; }
        }

        /// <summary>
        /// Cover url of the beatmap
        /// </summary>
        [JsonIgnore]
        public string CoverUrl
        {
            get { return $"https://assets.ppy.sh/beatmaps/{BeatmapsetId}/covers/cover.jpg"; }
        }

        /// <summary>
        /// Sound preview url of the beatmap
        /// </summary>
        [JsonIgnore]
        public string SoundPreviewUrl
        {
            get { return $"https://b.ppy.sh/preview/{BeatmapsetId}.mp3"; }
        }
    }
}