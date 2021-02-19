using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.Entities
{
    public class BeatmapsetCompact
    {
        [JsonProperty("artist")]
        public string Artist { get; internal set; }
        
        [JsonProperty("artist_unicode")]
        public string ArtistUnicode { get; internal set; }
        
        [JsonProperty("covers")]
        public Covers Covers { get; internal set; }
        
        [JsonProperty("creator")]
        public string Creator { get; internal set; }
        
        [JsonProperty("favorite_count")]
        public int FavoriteCount { get; internal set; }
        
        [JsonProperty("id")]
        public long Id { get; internal set; }
        
        [JsonProperty("play_count")]
        public int PlayCount { get; internal set; }
        
        [JsonProperty("preview_url")]
        public string PreviewUrl { get; internal set; }
        
        [JsonProperty("source")]
        public string Source { get; internal set; }
        
        [JsonProperty("status")]
        public string Status { get; internal set; }
        
        [JsonProperty("title")]
        public string Title { get; internal set; }
        
        [JsonProperty("title_unicode")]
        public string TitleUnicode { get; internal set; }
        
        [JsonProperty("user_id")]
        public long UserId { get; internal set; }
        
        [JsonProperty("video")]
        public bool HasVideo { get; internal set; }
        
        [JsonProperty("nsfw")]
        public bool Nsfw { get; internal set; }

        [JsonProperty("beatmaps")]
        public Optional<IReadOnlyList<Beatmap>> Beatmaps { get; internal set; }
        
        // todo: type
        [JsonProperty("converts")]
        public Optional<object> Converts { get; internal set; }
        
        // todo: type
        [JsonProperty("current_user_attributes")]
        public Optional<object> CurrentUserAttributes { get; internal set; }
        
        [JsonProperty("description")]
        public Optional<string> Description { get; internal set; }
        
        // todo: type
        [JsonProperty("discussions")]
        public Optional<object> Discussions { get; internal set; }
        
        // todo: type
        [JsonProperty("events")]
        public Optional<object> Events { get; internal set; }
        
        [JsonProperty("genre")]
        public Optional<string> Genre { get; internal set; }
        
        [JsonProperty("has_favourited")]
        public Optional<bool> HasFavourited { get; internal set; }

        [JsonProperty("language")]
        public Optional<string> Language { get; internal set; }
        
        // todo: type
        [JsonProperty("nominations")]
        public Optional<object> Nominations { get; internal set; }
        
        // todo: type
        [JsonProperty("rating")]
        public Optional<object> Rating { get; internal set; }
        
        // todo: type
        [JsonProperty("recent_favourites")]
        public Optional<object> RecentFavourites { get; internal set; }
        
        // todo: type
        [JsonProperty("related_users")]
        public Optional<object> RelatedUsers { get; internal set; }
        
        // todo: type
        [JsonProperty("user")]
        public Optional<object> User { get; internal set; }
        
        internal BeatmapsetCompact()
        {
            
        }
    }
}