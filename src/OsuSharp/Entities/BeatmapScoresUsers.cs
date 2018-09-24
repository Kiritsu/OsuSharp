using System.Collections.Generic;
using OsuSharp.Endpoints;

namespace OsuSharp.Entities
{
    public class BeatmapScoresUsers
    {
        public Beatmap Beatmap { get; set; }
        public List<Score> Scores { get; set; }
        public List<User> Users { get; set; }
    }
}