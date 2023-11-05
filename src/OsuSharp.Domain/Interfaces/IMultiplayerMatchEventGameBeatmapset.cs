using OsuSharp.Domain;
using OsuSharp.Interfaces;

namespace OsuSharp.Interfaces;

public interface IMultiplayerMatchEventGameBeatmapset
{
    string Artist { get; set; }
    string ArtistUnicode { get; set; }
    IBeatmapCover Covers { get; set; }
    string? Creator { get; set; }
    int FavouriteCount { get; set; }
    IBeatmapHype? Hype { get; set; }
    long Id { get; set; }
    bool Nsfw { get; set; }
    double Offset { get; set; }
    long PlayCount { get; set; }
    string? PreviewUrl { get; set; }
    string? Source { get; set; }
    bool Spotlight { get; set; }
    RankStatus Status { get; set; }
    string Title { get; set; }
    string TitleUnicode { get; set; }
    long? TrackId { get; set; }
    int? UserId { get; set; }
    bool Video { get; set; }
}