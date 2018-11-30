#pragma warning disable 0649
using System;
using Newtonsoft.Json;
using OsuSharp.Enums;
using OsuSharp.Misc;

namespace OsuSharp.Endpoints
{
    public sealed class Beatmap : Endpoint
    {
        [JsonProperty("approved")] 
        internal string _approved;

        /// <summary>
        ///     Id of the beatmapset
        /// </summary>
        [JsonProperty("beatmapset_id")]
        public long BeatmapsetId { get; internal set; }

        /// <summary>
        ///     Id of the beatmap
        /// </summary>
        [JsonProperty("beatmap_id")]
        public long BeatmapId { get; internal set; }

        /// <summary>
        ///     Enum that represent the state of the map
        /// </summary>
        [JsonIgnore]
        public BeatmapState State
            => Approved.ToBeatmapState(_approved);

        /// <summary>
        ///     Length of the beatmap (includes breaks)
        /// </summary>
        [JsonProperty("total_length")]
        public int TotalLength { get; internal set; }

        /// <summary>
        ///     Length of the beatmap (doesn't include breaks)
        /// </summary>
        [JsonProperty("hit_length")]
        public int HitLength { get; internal set; }

        /// <summary>
        ///     Difficulty of the beatmap
        /// </summary>
        [JsonProperty("version")]
        public string Difficulty { get; internal set; }

        /// <summary>
        ///     ?
        /// </summary>
        [JsonProperty("file_md5")]
        public string FileMd5 { get; internal set; }

        /// <summary>
        ///     Circle size (CS)
        /// </summary>
        [JsonProperty("diff_size")]
        public float CircleSize { get; internal set; }

        /// <summary>
        ///     Overall difficulty (OD)
        /// </summary>
        [JsonProperty("diff_overall")]
        public float OverallDifficulty { get; internal set; }

        /// <summary>
        ///     Approach rate (AR)
        /// </summary>
        [JsonProperty("diff_approach")]
        public float ApproachRate { get; internal set; }

        /// <summary>
        ///     Hp drain (HP)
        /// </summary>
        [JsonProperty("diff_drain")]
        public float HpDrain { get; internal set; }

        [JsonProperty("mode")] 
        internal int _mode;

        /// <summary>
        ///     Game mode of the map (standard/taiko/catch/mania)
        /// </summary>
        [JsonIgnore]
        public GameMode GameMode
            => (GameMode) Enum.Parse(typeof(GameMode), _mode.ToString());

        /// <summary>
        ///     Type of the map.
        /// </summary>
        [JsonIgnore]
        public BeatmapType BeatmapType { get; internal set; }

        /// <summary>
        ///     DateTime of beatmap's approval
        /// </summary>
        [JsonProperty("approved_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime ApprovedDate { get; internal set; }

        /// <summary>
        ///     DateTime of beatmap's latest update
        /// </summary>
        [JsonProperty("last_update", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime LastUpdate { get; internal set; }

        /// <summary>
        ///     Name of the artist
        /// </summary>
        [JsonProperty("artist")]
        public string Artist { get; internal set; }

        /// <summary>
        ///     Title of the beatmap
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; internal set; }

        /// <summary>
        ///     Name of the creator
        /// </summary>
        [JsonProperty("creator")]
        public string Creator { get; internal set; }

        /// <summary>
        ///     BPM of the beatmap
        /// </summary>
        [JsonProperty("bpm")]
        public float Bpm { get; internal set; }

        /// <summary>
        ///     Sources (added manually in bm editor)
        /// </summary>
        [JsonProperty("source")]
        public string Sources { get; internal set; }

        /// <summary>
        ///     Tags (added manually in bm editor)
        /// </summary>
        [JsonProperty("tags")]
        public string Tags { get; internal set; }

        /// <summary>
        ///     Genre id
        /// </summary>
        [JsonProperty("genre_id")]
        public int GenreId { get; internal set; }

        /// <summary>
        ///     Language id
        /// </summary>
        [JsonProperty("language_id")]
        public int LanguageId { get; internal set; }

        /// <summary>
        ///     Total count of favorites
        /// </summary>
        [JsonProperty("favourite_count")]
        public int FavouriteCount { get; internal set; }

        /// <summary>
        ///     Total count of playcounts
        /// </summary>
        [JsonProperty("playcount")]
        public int PlayCount { get; internal set; }

        /// <summary>
        ///     Total count of passcounts
        /// </summary>
        [JsonProperty("passcount")]
        public int PassCount { get; internal set; }

        /// <summary>
        ///     Probably the count of combo max (may be null, depending on the 'GameMode'
        /// </summary>
        [JsonProperty("max_combo", NullValueHandling = NullValueHandling.Ignore)]
        public int MaxCombo { get; internal set; }

        /// <summary>
        ///     Rating of the difficulty
        /// </summary>
        [JsonProperty("difficultyrating")]
        public float DifficultyRating { get; internal set; }

        /// <summary>
        ///     Thumbnail url of the beatmap
        /// </summary>
        [JsonIgnore]
        public string ThumbnailUrl
            => $"https://b.ppy.sh/thumb/{BeatmapsetId}l.jpg";

        /// <summary>
        ///     Cover url of the beatmap
        /// </summary>
        [JsonIgnore]
        public string CoverUrl
            => $"https://assets.ppy.sh/beatmaps/{BeatmapsetId}/covers/cover.jpg";

        /// <summary>
        ///     Sound preview url of the beatmap
        /// </summary>
        [JsonIgnore]
        public string SoundPreviewUrl
            => $"https://b.ppy.sh/preview/{BeatmapsetId}.mp3";

        internal Beatmap()
        {

        }
    }
}