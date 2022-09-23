using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public class RankingCountryRanking : IRankingCountryRanking
{
    public long ActiveUsers { get; internal set; }
    public string Code { get; internal set; } = null!;
    public IUserCountry Country { get; internal set; } = null!;
    public long Performance { get; internal set; }
    public long PlayCount { get; internal set; }
    public long RankedScore { get; internal set; }

    internal RankingCountryRanking() { }
}