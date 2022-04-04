using System.Collections.Generic;

namespace OsuSharp.Interfaces;

/// <summary>
/// Defines a compacted beatmapset object.
/// </summary>
public interface IBeatmapsetCompact : IClientEntity
{
    string Artist { get; }
    string ArtistUnicode { get; }
    IBeatmapCover Covers { get; }
    string Creator { get; }
    int FavoriteCount { get; }
    long Id { get; }
    int PlayCount { get; }
    string PreviewUrl { get; }
    string Source { get; }
    string Status { get; }
    string Title { get; }
    string TitleUnicode { get; }
    long UserId { get; }
    bool HasVideo { get; }
    bool Nsfw { get; }
    IReadOnlyList<IBeatmap> Beatmaps { get; }
    IReadOnlyList<IBeatmap> Converts { get; }
    object? CurrentUserAttributes { get; }
    object? Description { get; }
    object? Discussions { get; }
    object? Events { get; }
    object? Genre { get; }
    bool? HasFavourited { get; }
    object? Language { get; }
    object? Nominations { get; }
    object? Rating { get; }
    IReadOnlyList<IUserCompact> RecentFavourites { get; }
    object? RelatedUsers { get; }
    IUserCompact? User { get; }
}