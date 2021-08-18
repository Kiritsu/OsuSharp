using OsuSharp.Interfaces;
using System.Collections.Generic;

namespace OsuSharp.Domain
{
    public class BeatmapsetSearchEnumeration : IBeatmapsetSearchEnumeration
    {
        public IReadOnlyList<IBeatmapset> Beatmapsets { get; internal set; }
        public ICursor Cursor { get; internal set; }
        public ISearch Search { get; internal set; }
        public double? RecommendedDifficulty { get; internal set; }
        public object Error { get; internal set; }
        public long Total { get; internal set; }

        internal BeatmapsetSearchEnumeration()
        {

        }
    }
}
