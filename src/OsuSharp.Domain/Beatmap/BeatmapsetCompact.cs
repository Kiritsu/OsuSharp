using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public class BeatmapsetCompact : IBeatmapsetCompact
{
    public string Artist { get; internal set; } = null!;

    public string ArtistUnicode { get; internal set; } = null!;

    public IBeatmapCover Covers { get; internal set; } = null!;

    public string Creator { get; internal set; } = null!;

    public int FavoriteCount { get; internal set; }

    public long Id { get; internal set; }

    public int PlayCount { get; internal set; }

    public string PreviewUrl { get; internal set; } = null!;

    public string Source { get; internal set; } = null!;

    public string Status { get; internal set; } = null!;

    public string Title { get; internal set; } = null!;

    public string TitleUnicode { get; internal set; } = null!;

    public long UserId { get; internal set; }

    public bool HasVideo { get; internal set; }

    public bool Nsfw { get; internal set; }

    public IReadOnlyList<IBeatmap> Beatmaps { get; internal set; } = null!;
        
    public IReadOnlyList<IBeatmap> Converts { get; internal set; } = null!;

    // todo: type
    public object? CurrentUserAttributes { get; internal set; }

    // todo: type
    public object? Description { get; internal set; }

    // todo: type
    public object? Discussions { get; internal set; }

    // todo: type
    public object? Events { get; internal set; }

    // todo: type
    public object? Genre { get; internal set; }

    public bool? HasFavourited { get; internal set; }

    // todo: type
    public object? Language { get; internal set; }

    // todo: type
    public object? Nominations { get; internal set; } 

    // todo: type
    public object? Rating { get; internal set; }

    public IReadOnlyList<IUserCompact> RecentFavourites { get; internal set; } = null!;

    // todo: type
    public object? RelatedUsers { get; internal set; }

    public IUserCompact? User { get; internal set; }

    public IOsuClient Client { get; internal set; } = null!;

    internal BeatmapsetCompact()
    {
            
    }
}