namespace OsuSharp.Interfaces;

public interface IRankingCountryRanking
{
    long ActiveUsers { get; }
    string Code { get; }
    IUserCountry Country { get; }
    long Performance { get; }
    long PlayCount { get; }
    long RankedScore { get; }
}