using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public class BeatmapsetCompact : IBeatmapsetCompact
    {
        public string Artist { get; internal set; }

        public string ArtistUnicode { get; internal set; }

        public IBeatmapCover Covers { get; internal set; }

        public string Creator { get; internal set; }

        public int FavoriteCount { get; internal set; }

        public long Id { get; internal set; }

        public int PlayCount { get; internal set; }

        public string PreviewUrl { get; internal set; }

        public string Source { get; internal set; }

        public string Status { get; internal set; }

        public string Title { get; internal set; }

        public string TitleUnicode { get; internal set; }

        public long UserId { get; internal set; }

        public bool HasVideo { get; internal set; }

        public bool Nsfw { get; internal set; }

        public IReadOnlyList<IBeatmap> Beatmaps { get; internal set; }

        // todo: type
        public IReadOnlyList<IBeatmap> Converts { get; internal set; }

        // todo: type
        public object CurrentUserAttributes { get; internal set; }

        public object Description { get; internal set; }

        // todo: type
        public object Discussions { get; internal set; }

        // todo: type
        public object Events { get; internal set; }

        public object Genre { get; internal set; }

        public bool? HasFavourited { get; internal set; }

        public object Language { get; internal set; }

        // todo: type
        public object Nominations { get; internal set; }

        // todo: type
        public object Rating { get; internal set; }

        // todo: type
        public IReadOnlyList<IUserCompactBase> RecentFavourites { get; internal set; }

        // todo: type
        public object RelatedUsers { get; internal set; }

        // todo: type
        public IUserCompactBase User { get; internal set; }
        
        internal BeatmapsetCompact()
        {
            
        }
    }
}