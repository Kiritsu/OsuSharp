using System.Collections.Generic;

namespace OsuSharp.Interfaces;

public interface IBeatmapScores
{
    IReadOnlyList<IScore> Scores { get; }
    IBeatmapUserScore UserScore { get; }
}