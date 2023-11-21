using OsuSharp.Domain;
using OsuSharp.Interfaces;

namespace OsuSharp.Interfaces;

public interface IMultiplayerMatchEventGameBeatmapset
{
    string Artist { get; }
    string ArtistUnicode { get; }
    IBeatmapCover Covers { get; }
    string? Creator { get; }
    int FavouriteCount { get; }
    IBeatmapHype? Hype { get; }
    long Id { get; }
    bool Nsfw { get; }
    double Offset { get; }
    long PlayCount { get; }
    string? PreviewUrl { get; }
    string? Source { get; }
    bool Spotlight { get; }
    RankStatus Status { get; }
    string Title { get; }
    string TitleUnicode { get; }
    long? TrackId { get; }
    int? UserId { get; }
    bool Video { get; }
}