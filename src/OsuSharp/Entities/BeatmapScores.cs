using System.Collections.Generic;
using OsuSharp.Endpoints;

namespace OsuSharp.Entities
{
    public sealed class BeatmapScores
    {
        public Beatmap Beatmap { get; internal set; }
        public IReadOnlyList<Score> Score { get; internal set; }

        internal BeatmapScores()
        {

        }
    }
}