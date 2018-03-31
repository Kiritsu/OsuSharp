using System.Collections.Generic;
using OsuSharp.BeatmapsEndpoint;
using OsuSharp.ScoreEndpoint;
using OsuSharp.UserEndpoint;

namespace OsuSharp.Entities
{
    public class BeatmapScoresUsers
    {
        public Beatmap Beatmap { get; set; }
        public List<Score> Scores { get; set; }
        public List<User> Users { get; set; }
    }
}
