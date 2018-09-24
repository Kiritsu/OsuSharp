using System.Collections.Generic;
using OsuSharp.Endpoints;

namespace OsuSharp.Entities
{
    public class BeatmapScores
    {
        public Beatmap Beatmap { get; set; }
        public List<Score> Score { get; set; }
    }
}