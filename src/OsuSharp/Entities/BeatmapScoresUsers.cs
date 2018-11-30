using System.Collections.Generic;
using OsuSharp.Endpoints;

namespace OsuSharp.Entities
{
    public sealed class BeatmapScoresUsers
    {
        public Beatmap Beatmap { get; internal set; }
        public IReadOnlyList<Score> Scores { get; internal set; }
        public IReadOnlyList<User> Users { get; internal set; }

        internal BeatmapScoresUsers()
        {

        }
    }
}