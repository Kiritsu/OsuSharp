using OsuSharp.BeatmapsEndpoint;
using OsuSharp.UserRecentEndpoint;

namespace OsuSharp.Entities
{
    public class UserRecentBeatmap
    {
        public Beatmaps Beatmap { get; set; }
        public UserRecent UserRecent { get; set; }
    }
}
