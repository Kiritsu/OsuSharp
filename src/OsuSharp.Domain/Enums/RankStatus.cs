namespace OsuSharp.Domain;

/// <summary>
/// Represents the ranking status of a beatmapset.
/// </summary>
public enum RankStatus
{
    Graveyard = -2,
    Wip = -1,
    Pending = 0,
    Ranked = 1,
    Approved = 2,
    Qualified = 3,
    Loved = 4
}