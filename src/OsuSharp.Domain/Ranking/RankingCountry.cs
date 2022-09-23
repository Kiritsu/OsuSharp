using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain.Ranking;

public class RankingCountry : IRankingCountry
{
	public IRankingCursor Cursor { get; internal set; } = null!;
	public IReadOnlyList<IRankingCountryRanking> Ranking { get; internal set; } = null!;
	public int Total { get; internal set; }
}