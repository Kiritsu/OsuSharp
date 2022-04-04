using OsuSharp.Interfaces;
using System.Collections.Generic;

namespace OsuSharp.Domain;

public sealed class BeatmapScores : IBeatmapScores
{
    public IReadOnlyList<IScore> Scores { get; internal set; } = null!;

    public IBeatmapUserScore UserScore { get; internal set; } = null!;

    internal BeatmapScores()
    {

    }
}