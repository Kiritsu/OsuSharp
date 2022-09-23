using System.Collections.Generic;

namespace OsuSharp.Interfaces;

public interface IRankingCountry : IClientEntity
{
    IRankingCursor Cursor { get; }
    IReadOnlyList<IRankingCountryRanking> Ranking { get; }
    int Total { get; }
}