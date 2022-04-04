using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface IOsuToken
{
    /// <summary>
    /// Gets the type of the token.
    /// </summary>
    TokenType Type { get; }

    /// <summary>
    /// Gets the access token.
    /// </summary>
    string AccessToken { get; }

    /// <summary>
    /// Gets the refresh token.
    /// </summary>
    string RefreshToken { get; }

    /// <summary>
    /// Gets whether the token is revoked. Cannot be tracked by the API and is manually updated by the library.
    /// </summary>
    bool Revoked { get; }

    /// <summary>
    /// Gets the amount of time in seconds of the validity of the token.
    /// </summary>
    long ExpiresInSeconds { get; }
}