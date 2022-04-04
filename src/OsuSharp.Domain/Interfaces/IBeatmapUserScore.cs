namespace OsuSharp.Interfaces;

/// <summary>
/// Defines the object of the score of a user for a beatmap.
/// </summary>
public interface IBeatmapUserScore
{
    /// <summary>
    /// Gets the position of the score in the leaderboard.
    /// </summary>
    int Position { get; }

    /// <summary>
    /// Gets the score done by the user.
    /// </summary>
    IScore Score { get; }
}