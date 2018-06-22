using System.Collections.Generic;
using OsuSharp.BeatmapEndpoint;
using OsuSharp.ScoreEndpoint;

namespace OsuSharp.Entities
{
    public class BeatmapScores
    {
        public Beatmap Beatmap { get; set; }
        public List<Score> Score { get; set; }
    }
}
