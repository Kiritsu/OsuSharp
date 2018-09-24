using OsuSharp.Endpoints;

namespace OsuSharp.Entities
{
    public class UserRecentBeatmap
    {
        public Beatmap Beatmap { get; set; }
        public UserRecent UserRecent { get; set; }
    }
}