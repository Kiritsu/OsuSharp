using OsuSharp.BeatmapEndpoint;
using OsuSharp.UserBestEndpoint;

namespace OsuSharp.Entities
{
    public class UserBestBeatmap
    {
        public Beatmap Beatmap { get; set; }
        public UserBest UserBest { get; set; }
    }
}
