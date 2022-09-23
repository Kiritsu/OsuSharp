using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public class RankingScore : IRankingScore
{
    public IRankingCursor Cursor { get; internal set; } = null!;
    public IReadOnlyList<IUserStatistics> Ranking { get; internal set; } = null!;
    public int Total { get; internal set; }
    public IOsuClient Client { get; internal set; } = null!;

    internal RankingScore() { }
}