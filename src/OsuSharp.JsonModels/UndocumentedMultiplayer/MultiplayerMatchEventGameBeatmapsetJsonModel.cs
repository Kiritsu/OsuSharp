using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class MultiplayerMatchEventGameBeatmapsetJsonModel : JsonModel
{
    [JsonProperty("artist")]
    public string Artist { get; set; } = null!;
    
    [JsonProperty("artist_unicode")]
    public string ArtistUnicode { get; set; } = null!;
    
    [JsonProperty("covers")]
    public BeatmapCoverJsonModel Covers { get; set; } = null!;

    [JsonProperty("creator")]
    public string? Creator { get; set; }
    
    [JsonProperty("favourite_count")]
    public int FavouriteCount { get; set; }
    
    [JsonProperty("hype")]
    public BeatmapHypeJsonModel? Hype { get; set; }

    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("nsfw")]
    public bool Nsfw { get; set; }
    
    [JsonProperty("offset")]
    public double Offset { get; set; }
    
    [JsonProperty("play_count")]
    public long PlayCount { get; set; }
    
    [JsonProperty("preview_url")]
    public string? PreviewUrl { get; set; }
    [JsonProperty("source")]
    public string? Source { get; set; }
    
    [JsonProperty("spotlight")]
    public bool Spotlight { get; set; }
    
    [JsonProperty("status")]
    public string Status { get; set; } = null!;

    [JsonProperty("title")] 
    public string Title { get; set; } = null!;
    
    [JsonProperty("title_unicode")]
    public string TitleUnicode { get; set; } = null!;
    
    [JsonProperty("track_id")]
    public long? TrackId { get; set; }
    
    [JsonProperty("user_id")]
    public int? UserId { get; set; }
    
    [JsonProperty("video")]
    public bool Video { get; set; }
}