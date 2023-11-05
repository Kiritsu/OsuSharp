using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public class MultiplayerMatchEventGameBeatmapset : IMultiplayerMatchEventGameBeatmapset
{
    public string Artist { get; set; } = null!;
    public string ArtistUnicode { get; set; } = null!;
    public IBeatmapCover Covers { get; set; } = null!;
    public string? Creator { get; set; }
    public int FavouriteCount { get; set; }
    public IBeatmapHype? Hype { get; set; }
    public long Id { get; set; }
    public bool Nsfw { get; set; }
    public double Offset { get; set; }
    public long PlayCount { get; set; }
    public string? PreviewUrl { get; set; }
    public string? Source { get; set; }
    public bool Spotlight { get; set; }
    public RankStatus Status { get; set; }
    public string Title { get; set; } = null!;
    public string TitleUnicode { get; set; } = null!;
    public long? TrackId { get; set; }
    public int? UserId { get; set; }
    public bool Video { get; set; }

    internal MultiplayerMatchEventGameBeatmapset()
    {
        
    }
}