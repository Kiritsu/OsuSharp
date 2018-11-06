using OsuSharp.Endpoints;

namespace OsuSharp.Entities
{
    public sealed class UserBestBeatmap
    {
        public Beatmap Beatmap { get; internal set; }
        public UserBest UserBest { get; internal set; }
    }
}