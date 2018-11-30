using OsuSharp.Endpoints;

namespace OsuSharp.Entities
{
    public sealed class UserRecentBeatmap
    {
        public Beatmap Beatmap { get; internal set; }
        public UserRecent UserRecent { get; internal set; }

        internal UserRecentBeatmap()
        {

        }
    }
}