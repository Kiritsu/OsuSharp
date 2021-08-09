using System.Collections.Generic;

namespace OsuSharp.Interfaces
{
    public interface IBeatmapsetSearchEnumeration
    {
        IReadOnlyList<IBeatmapset> Beatmapsets { get; }
        ICursor Cursor { get; }
        object Error { get; }
        double? RecommendedDifficulty { get; }
        ISearch Search { get; }
        long Total { get; }
    }
}