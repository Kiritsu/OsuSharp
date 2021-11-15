using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class BeatmapsetJsonModel : JsonModel
    {
        [JsonProperty("artist")]
        public string Artist { get; set; }

        [JsonProperty("artist_unicode")]
        public string ArtistUnicode { get; set; }

        [JsonProperty("covers")]
        public BeatmapCoverJsonModel Covers { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }

        [JsonProperty("favourite_count")]
        public int FavoriteCount { get; set; }

        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("play_count")]
        public int PlayCount { get; set; }

        [JsonProperty("preview_url")]
        public string PreviewUrl { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("title_unicode")]
        public string TitleUnicode { get; set; }

        [JsonProperty("user_id")]
        public long UserId { get; set; }

        [JsonProperty("video")]
        public bool HasVideo { get; set; }

        [JsonProperty("nsfw")]
        public bool Nsfw { get; set; }

        [JsonProperty("beatmaps")]
        public List<BeatmapJsonModel> Beatmaps { get; set; }

        // todo: type
        [JsonProperty("converts")]
        public List<BeatmapJsonModel> Converts { get; set; }

        // todo: type
        [JsonProperty("current_user_attributes")]
        public object CurrentUserAttributes { get; set; }

        [JsonProperty("description")]
        public object Description { get; set; }

        // todo: type
        [JsonProperty("discussions")]
        public object Discussions { get; set; }

        // todo: type
        [JsonProperty("events")]
        public object Events { get; set; }

        [JsonProperty("genre")]
        public object Genre { get; set; }

        [JsonProperty("has_favourited")]
        public bool? HasFavourited { get; set; }

        [JsonProperty("language")]
        public object Language { get; set; }

        // todo: type
        [JsonProperty("nominations")]
        public object Nominations { get; set; }

        // todo: type
        [JsonProperty("rating")]
        public object Rating { get; set; }

        // todo: type
        [JsonProperty("recent_favourites")]
        public List<UserJsonModel> RecentFavourites { get; set; }

        // todo: type
        [JsonProperty("related_users")]
        public object RelatedUsers { get; set; }

        // todo: type
        [JsonProperty("user")]
        public UserJsonModel User { get; set; }

        [JsonProperty("availability")]
        public BeatmapAvailabilityJsonModel Availability { get; set; }

        [JsonProperty("bpm")]
        public double Bpm { get; set; }

        [JsonProperty("can_be_hyped")]
        public bool CanBeHyped { get; set; }

        [JsonProperty("discussion_enabled")]
        public bool DiscussionEnabled { get; set; }

        [JsonProperty("discussion_locked")]
        public bool DiscussionLocked { get; set; }

        [JsonProperty("hype")]
        public BeatmapHypeJsonModel Hype { get; set; }

        [JsonProperty("is_scoreable")]
        public bool IsScoreable { get; set; }

        [JsonProperty("last_updated")]
        public DateTimeOffset LastUpdated { get; set; }

        [JsonProperty("legacy_thread_url")]
        public string LegacyThreadUrl { get; set; }

        [JsonProperty("nominations_summary")]
        public BeatmapNominationJsonModel Nomination { get; set; }

        [JsonProperty("ranked")]
        public string Ranked { get; set; }

        [JsonProperty("ranked_date")]
        public DateTimeOffset? RankedDate { get; set; }

        [JsonProperty("storyboard")]
        public bool HasStoryboard { get; set; }

        [JsonProperty("submitted_date")]
        public DateTimeOffset? SubmittedAt { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("ratings")]
        public List<int> Ratings { get; set; }
    }
}