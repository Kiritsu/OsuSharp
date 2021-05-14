using System.Collections.Generic;

namespace OsuSharp.Interfaces
{
    public interface IBeatmapsetCompact
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
        object Converts { get; }
        object CurrentUserAttributes { get; }
        string Description { get; }
        object Discussions { get; }
        object Events { get; }
        string Genre { get; }
        bool? HasFavourited { get; }
        string Language { get; }
        object Nominations { get; }
        object Rating { get; }
        object RecentFavourites { get; }
        object RelatedUsers { get; }
        object User { get; }
    }
}