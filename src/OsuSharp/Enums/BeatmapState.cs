namespace OsuSharp.Misc
{
    /// <summary>
    ///     Enum representing the different possible states of a beatmap
    /// </summary>
    public enum BeatmapState
    {
        Graveyard = -2,
        Wip = -1,
        Pending = 0,
        Ranked = 1,
        Approved = 2,
        Qualified = 3,
        Loved = 4,
        Unknown = 5
    }
}