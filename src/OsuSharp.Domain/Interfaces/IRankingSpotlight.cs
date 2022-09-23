using System.Collections.Generic;

namespace OsuSharp.Interfaces;

public interface IRankingSpotlight : IClientEntity
{
    IReadOnlyList<IBeatmapset> Beatmapsets { get; }
    IRankingCursor Cursor { get; }
    IReadOnlyList<IUserStatistics> Ranking { get; }
    IRankingSpotlightInformation Spotlight { get; }
    int Total { get; }
}