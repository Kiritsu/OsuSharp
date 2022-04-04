namespace OsuSharp.Interfaces;

/// <summary>
/// Represents an achievement's user.
/// </summary>
public interface IEventUserModel
{
    /// <summary>
    /// Gets the username of the user.
    /// </summary>
    string Username { get; }

    string Url { get; }
}