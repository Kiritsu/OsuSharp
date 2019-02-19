using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OsuSharp.Enums;

namespace OsuSharp.Entities
{
    public sealed class Beatmap
    {
        internal Beatmap() { }

        [JsonProperty("beatmapset_id")]
        public long BeatmapsetId { get; internal set; }

        [JsonProperty("beatmap_id")]
        public long BeatmapId { get; internal set; }

        [JsonProperty("approved")]
        public BeatmapState State { get; internal set; }

        [JsonProperty("total_length")]
        private readonly int totalLength;

        [JsonIgnore]
        public TimeSpan TotalLength => TimeSpan.FromSeconds(totalLength);

        [JsonProperty("hit_length")]
        private readonly int hitLength;

        [JsonIgnore]
        public TimeSpan HitLength => TimeSpan.FromSeconds(hitLength);

        [JsonProperty("version")]
        public string Difficulty { get; internal set; }

        [JsonProperty("file_md5")]
        public string FileMd5 { get; internal set; }

        [JsonProperty("diff_size")]
        public int CircleSize { get; internal set; }

        [JsonProperty("diff_overall")]
        public int OverallDifficulty { get; internal set; }

        [JsonProperty("diff_approach")]
        public int ApproachRate { get; internal set; }

        [JsonProperty("diff_drain")]
        public int HpDrain { get; internal set; }

        [JsonProperty("mode")]
        public GameMode GameMode { get; internal set; }

        [JsonProperty("approved_date", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset ApprovedDate { get; internal set; }

        [JsonProperty("last_update", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset LastUpdate { get; internal set; }

        [JsonProperty("artist")]
        public string Artist { get; internal set; }

        [JsonProperty("title")]
        public string Title { get; internal set; }

        [JsonProperty("creator")]
        public string Author { get; internal set; }

        [JsonProperty("creator_id")]
        public long AuthorId { get; internal set; }

        [JsonProperty("bpm")]
        public double Bpm { get; internal set; }

        [JsonProperty("source")]
        public string Source { get; internal set; }

        [JsonProperty("tags")]
        private readonly string _tags;

        [JsonIgnore]
        public IReadOnlyList<string> Tags => _tags.Split(' ');

        [JsonProperty("genre_id")]
        public BeatmapGenre Genre { get; internal set; }

        [JsonProperty("language_id")]
        public BeatmapLanguage Language { get; internal set; }

        [JsonProperty("favorite_count")]
        public int FavoriteCount { get; internal set; }

        [JsonProperty("playcount")]
        public int PlayCount { get; internal set; }

        [JsonProperty("passcount")]
        public int PassCount { get; internal set; }

        [JsonProperty("max_combo", NullValueHandling = NullValueHandling.Ignore)]
        public int MaxCombo { get; internal set; }

        [JsonProperty("difficultyrating")]
        public double DifficultyRating { get; internal set; }

        [JsonIgnore]
        public Uri ThumbnailUri => new Uri($"https://b.ppy.sh/thumb/{BeatmapsetId}l.jpg");

        [JsonIgnore]
        public Uri CoverUri => new Uri($"https://assets.ppy.sh/beatmaps/{BeatmapsetId}/covers/cover.jpg");

        [JsonIgnore]
        public Uri SoundPreviewUri => new Uri($"https://b.ppy.sh/preview/{BeatmapsetId}.mp3");

        [JsonIgnore]
        public Uri OsuDirectDownloadUri => new Uri($"osu://dl/{BeatmapsetId}");
    }
}
