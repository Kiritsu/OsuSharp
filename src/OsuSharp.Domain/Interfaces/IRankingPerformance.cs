using System.Collections.Generic;

namespace OsuSharp.Interfaces;

public interface IRankingPerformance : IClientEntity
{
    IRankingCursor Cursor { get; }
    IReadOnlyList<IUserStatistics> Ranking { get; }
    int Total { get; }
}