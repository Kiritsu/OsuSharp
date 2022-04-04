using OsuSharp.Interfaces;
using System.Collections.Generic;

namespace OsuSharp.Domain;

public class BeatmapsetSearchEnumeration : IBeatmapsetSearchEnumeration
{
    public IReadOnlyList<IBeatmapset> Beatmapsets { get; internal set; } = null!;
    public ICursor Cursor { get; internal set; } = null!;
    public ISearch Search { get; internal set; } = null!;
    public double? RecommendedDifficulty { get; internal set; }
    public object Error { get; internal set; } = null!;
    public long Total { get; internal set; }
    public string CursorString { get; set; } = null!;

    internal BeatmapsetSearchEnumeration()
    {

    }
}