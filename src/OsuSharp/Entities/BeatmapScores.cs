using System.Collections.Generic;
using OsuSharp.BeatmapsEndpoint;
using OsuSharp.ScoreEndpoint;

namespace OsuSharp.Entities
{
    public class BeatmapScores
    {
        public Beatmap Beatmap { get; set; }
        public List<Score> Score { get; set; }
    }
}
