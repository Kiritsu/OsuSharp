using System;
using System.Collections.Generic;
using System.Text;
using OsuSharp.BeatmapsEndpoint;
using OsuSharp.ScoreEndpoint;
using OsuSharp.UserEndpoint;

namespace OsuSharp.Entities
{
    public class BeatmapScoresUsers
    {
        public Beatmaps Beatmap { get; set; }
        public List<Scores> Scores { get; set; }
        public List<Users> Users { get; set; }
    }
}
