using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public class RankingSpotlight : IRankingSpotlight
{
    public IReadOnlyList<IBeatmapset> Beatmapsets { get; internal set; } = null!;
    public IRankingCursor Cursor { get; internal set; } = null!;
    public IReadOnlyList<IUserStatistics> Ranking { get; internal set; } = null!;
    public IRankingSpotlightInformation Spotlight { get; internal set; } = null!;
    public int Total { get; internal set; }
    public IOsuClient Client { get; internal set; } = null!;

    internal RankingSpotlight() { }
}