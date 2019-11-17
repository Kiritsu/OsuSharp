﻿#pragma warning disable CS0649
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace OsuSharp
{
    public sealed class Beatmap : EntityBase
    {
        internal Beatmap() { }

        /// <summary>
        ///     Id of the beatmapset this beatmap belongs to.
        /// </summary>
        [JsonProperty("beatmapset_id")]
        public long BeatmapsetId { get; internal set; }

        /// <summary>
        ///     Id of the beatmap.
        /// </summary>
        [JsonProperty("beatmap_id")]
        public long BeatmapId { get; internal set; }

        /// <summary>
        ///     Indicates the current approval state of the beatmap.
        /// </summary>
        [JsonProperty("approved")]
        public BeatmapState State { get; internal set; }

        [JsonProperty("total_length")]
        private readonly int totalLength;

        /// <summary>
        ///     Total length of the beatmap. (including breaks)
        /// </summary>
        [JsonIgnore]
        public TimeSpan TotalLength => TimeSpan.FromSeconds(totalLength);

        [JsonProperty("hit_length")]
        private readonly int hitLength;

        /// <summary>
        ///     Total hit length of the beatmaps. (doesn't include breaks)
        /// </summary>
        [JsonIgnore]
        public TimeSpan HitLength => TimeSpan.FromSeconds(hitLength);

        /// <summary>
        ///     Name of this beatmap's difficulty.
        /// </summary>
        [JsonProperty("version")]
        public string Difficulty { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("file_md5")]
        public string FileMd5 { get; internal set; }

        /// <summary>
        ///     Size of the circles.
        /// </summary>
        [JsonProperty("diff_size")]
        public double CircleSize { get; internal set; }

        /// <summary>
        ///     Overall difficulty level.
        /// </summary>
        [JsonProperty("diff_overall")]
        public double OverallDifficulty { get; internal set; }

        /// <summary>
        ///     Approach rate level.
        /// </summary>
        [JsonProperty("diff_approach")]
        public double ApproachRate { get; internal set; }

        /// <summary>
        ///     Hp drain level.
        /// </summary>
        [JsonProperty("diff_drain")]
        public double HpDrain { get; internal set; }

        /// <summary>
        ///     Game mode this map is made for.
        /// </summary>
        [JsonProperty("mode")]
        public GameMode GameMode { get; internal set; }

        /// <summary>
        ///     Date time this map was approved. Null if the map has not been approved.
        /// </summary>
        [JsonProperty("approved_date")]
        public DateTimeOffset? ApprovedDate { get; internal set; }

        /// <summary>
        ///     Date time this map was updated.
        /// </summary>
        [JsonProperty("last_update")]
        public DateTimeOffset? LastUpdate { get; internal set; }

        /// <summary>
        ///     Name of the artist.
        /// </summary>
        [JsonProperty("artist")]
        public string Artist { get; internal set; }

        /// <summary>
        ///     Title of the map.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get; internal set; }

        /// <summary>
        ///     Username of the author.
        /// </summary>
        [JsonProperty("creator")]
        public string Author { get; internal set; }

        /// <summary>
        ///     Id of the author.
        /// </summary>
        [JsonProperty("creator_id")]
        public long AuthorId { get; internal set; }

        /// <summary>
        ///     Bpm of the map.
        /// </summary>
        [JsonProperty("bpm")]
        public double Bpm { get; internal set; }

        /// <summary>
        ///     Source of the map.
        /// </summary>
        [JsonProperty("source")]
        public string Source { get; internal set; }

        [JsonProperty("tags")]
        private readonly string _tags;

        /// <summary>
        ///     Tags given to this map.
        /// </summary>
        [JsonIgnore]
        public IReadOnlyList<string> Tags => _tags?.Split(' ');

        /// <summary>
        ///     Genre of the beatmap.
        /// </summary>
        [JsonProperty("genre_id")]
        public BeatmapGenre Genre { get; internal set; }

        /// <summary>
        ///     Language of the beatmap.
        /// </summary>
        [JsonProperty("language_id")]
        public BeatmapLanguage Language { get; internal set; }

        /// <summary>
        ///     Amount of favorites.
        /// </summary>
        [JsonProperty("favorite_count")]
        public int FavoriteCount { get; internal set; }

        /// <summary>
        ///     Amount of plays.
        /// </summary>
        [JsonProperty("playcount")]
        public int? PlayCount { get; internal set; }

        /// <summary>
        ///     Amount of pass.
        /// </summary>
        [JsonProperty("passcount")]
        public int? PassCount { get; internal set; }

        /// <summary>
        ///     Amount of fails.
        /// </summary>
        [JsonIgnore]
        public int? FailCount => PlayCount - PassCount;

        /// <summary>
        ///     Maximum combo. Can be null depending on the <see cref="GameMode"/>.
        /// </summary>
        [JsonProperty("max_combo")]
        public int? MaxCombo { get; internal set; }

        /// <summary>
        ///     Star rating of the beatmap.
        /// </summary>
        [JsonProperty("difficultyrating")]
        public double DifficultyRating { get; internal set; }

        /// <summary>
        ///     Uri redirecting to the Thumbnail of the beatmap.
        /// </summary>
        [JsonIgnore]
        public Uri ThumbnailUri => new Uri($"https://b.ppy.sh/thumb/{BeatmapsetId}l.jpg");

        /// <summary>
        ///     Uri redirecting to the Cover of the beatmap.
        /// </summary>
        [JsonIgnore]
        public Uri CoverUri => new Uri($"https://assets.ppy.sh/beatmaps/{BeatmapsetId}/covers/cover.jpg");

        /// <summary>
        ///     Uri redirecting to the Sound Preview of the beatmap.
        /// </summary>
        [JsonIgnore]
        public Uri SoundPreviewUri => new Uri($"https://b.ppy.sh/preview/{BeatmapsetId}.mp3");

        /// <summary>
        ///     Uri redirecting to the in-game osu!direct download.
        /// </summary>
        [JsonIgnore]
        public Uri OsuDirectDownloadUri => new Uri($"osu://dl/{BeatmapsetId}");

        /// <summary>
        ///     Uri redirecting to the beatmap (.osu) file.
        /// </summary>
        public Uri BeatmapDownloadUri => new Uri($"https://osu.ppy.sh/osu/{BeatmapId}");

        /// <summary>
        ///     Gets the entire beatmapset of this beatmap.
        /// </summary>
        /// <returns></returns>
        public Task<IReadOnlyList<Beatmap>> GetBeatmapsetAsync()
        {
            return Client.GetBeatmapsetAsync(BeatmapsetId, GameMode);
        }

        /// <summary>
        ///     Gets the author of the beatmap.
        /// </summary>
        /// <returns></returns>
        public Task<User> GetAuthorAsync(GameMode? gameMode = null)
        {
            return Client.GetUserByUserIdAsync(AuthorId, gameMode ?? GameMode);
        }
    }
}
