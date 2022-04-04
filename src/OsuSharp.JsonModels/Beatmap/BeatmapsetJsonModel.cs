using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class BeatmapsetJsonModel : JsonModel
{
    [JsonProperty("artist")]
    public string Artist { get; set; } = null!;

    [JsonProperty("artist_unicode")]
    public string ArtistUnicode { get; set; } = null!;

    [JsonProperty("covers")]
    public BeatmapCoverJsonModel Covers { get; set; } = null!;

    [JsonProperty("creator")]
    public string Creator { get; set; } = null!;

    [JsonProperty("favourite_count")]
    public int FavoriteCount { get; set; }

    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("play_count")]
    public int PlayCount { get; set; }

    [JsonProperty("preview_url")]
    public string PreviewUrl { get; set; } = null!;

    [JsonProperty("source")]
    public string Source { get; set; } = null!;

    [JsonProperty("status")]
    public string Status { get; set; } = null!;

    [JsonProperty("title")]
    public string Title { get; set; } = null!;

    [JsonProperty("title_unicode")]
    public string TitleUnicode { get; set; } = null!;

    [JsonProperty("user_id")]
    public long UserId { get; set; }

    [JsonProperty("video")]
    public bool HasVideo { get; set; }

    [JsonProperty("nsfw")]
    public bool Nsfw { get; set; }

    [JsonProperty("beatmaps")] 
    public List<BeatmapJsonModel> Beatmaps { get; set; } = new();
        
    [JsonProperty("converts")]
    public List<BeatmapJsonModel> Converts { get; set; } = new();

    // todo: type
    [JsonProperty("current_user_attributes")]
    public object? CurrentUserAttributes { get; set; }

    // todo: type
    [JsonProperty("description")]
    public object? Description { get; set; }

    // todo: type
    [JsonProperty("discussions")]
    public object? Discussions { get; set; }

    // todo: type
    [JsonProperty("events")]
    public object? Events { get; set; }

    // todo: type
    [JsonProperty("genre")]
    public object? Genre { get; set; }

    // todo: type
    [JsonProperty("has_favourited")]
    public bool? HasFavourited { get; set; }

    // todo: type
    [JsonProperty("language")]
    public object? Language { get; set; }

    // todo: type
    [JsonProperty("nominations")]
    public object? Nominations { get; set; }

    // todo: type
    [JsonProperty("rating")]
    public object? Rating { get; set; }
        
    [JsonProperty("recent_favourites")]
    public List<UserJsonModel> RecentFavourites { get; set; } = null!;

    // todo: type
    [JsonProperty("related_users")]
    public object? RelatedUsers { get; set; }

    [JsonProperty("user")]
    public UserJsonModel User { get; set; } = null!;

    [JsonProperty("availability")]
    public BeatmapAvailabilityJsonModel Availability { get; set; } = null!;

    [JsonProperty("bpm")]
    public double Bpm { get; set; }

    [JsonProperty("can_be_hyped")]
    public bool CanBeHyped { get; set; }

    [JsonProperty("discussion_enabled")]
    public bool DiscussionEnabled { get; set; }

    [JsonProperty("discussion_locked")]
    public bool DiscussionLocked { get; set; }

    [JsonProperty("hype")]
    public BeatmapHypeJsonModel Hype { get; set; } = null!;

    [JsonProperty("is_scoreable")]
    public bool IsScoreable { get; set; }

    [JsonProperty("last_updated")]
    public DateTimeOffset LastUpdated { get; set; }

    [JsonProperty("legacy_thread_url")]
    public string LegacyThreadUrl { get; set; } = null!;

    [JsonProperty("nominations_summary")]
    public BeatmapNominationJsonModel Nomination { get; set; } = null!;

    [JsonProperty("ranked")]
    public string Ranked { get; set; } = null!;

    [JsonProperty("ranked_date")]
    public DateTimeOffset? RankedDate { get; set; }

    [JsonProperty("storyboard")]
    public bool HasStoryboard { get; set; }

    [JsonProperty("submitted_date")]
    public DateTimeOffset? SubmittedAt { get; set; }

    [JsonProperty("tags")]
    public string Tags { get; set; } = null!;

    [JsonProperty("ratings")]
    public List<int> Ratings { get; set; } = null!;

    [JsonProperty("track_id")]
    public long? TrackId { get; set; }
}