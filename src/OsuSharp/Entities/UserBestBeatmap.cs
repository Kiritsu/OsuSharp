using OsuSharp.Endpoints;

namespace OsuSharp.Entities
{
    public class UserBestBeatmap
    {
        public Beatmap Beatmap { get; set; }
        public UserBest UserBest { get; set; }
    }
}