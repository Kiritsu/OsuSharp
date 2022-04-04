namespace OsuSharp.Interfaces;

/// <summary>
/// Defines an access token response.
/// </summary>
public interface IAccessTokenResponse
{
    /// <summary>
    /// Gets the type of the token.
    /// </summary>
    string TokenType { get; }

    /// <summary>
    /// Gets the amount of seconds before the access token expires.
    /// </summary>
    long ExpiresIn { get; }

    /// <summary>
    /// Gets the access token.
    /// </summary>
    string AccessToken { get; }

    /// <summary>
    /// Gets the refresh token.
    /// </summary>
    string RefreshToken { get; }
}