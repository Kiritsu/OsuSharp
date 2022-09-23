using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public class RankingCursor : IRankingCursor
{
    public int? Page { get; internal set; } = null!;

    internal RankingCursor() { }
}