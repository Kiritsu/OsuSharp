using System.Collections.Generic;
using OsuSharp.BeatmapsEndpoint;
using OsuSharp.ScoreEndpoint;

namespace OsuSharp.Entities
{
    public class BeatmapScores
    {
        public Beatmaps Beatmap { get; set; }
        public List<Scores> Score { get; set; }
    }
}
