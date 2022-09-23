using OsuSharp.Interfaces;

namespace OsuSharp.Domain.Ranking;

public class RankingCursor : IRankingCursor
{
	public int? Page { get; internal set; } = null!;
}