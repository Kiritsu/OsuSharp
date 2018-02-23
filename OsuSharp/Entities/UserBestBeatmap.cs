using OsuSharp.BeatmapsEndpoint;
using OsuSharp.UserBestEndpoint;

namespace OsuSharp.Entities
{
    public class UserBestBeatmap
    {
        public Beatmaps Beatmap { get; set; }
        public UserBest UserBest { get; set; }
    }
}
