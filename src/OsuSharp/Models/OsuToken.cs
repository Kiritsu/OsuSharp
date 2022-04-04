using System;
using OsuSharp.Domain;
using OsuSharp.Interfaces;

namespace OsuSharp.Models;

/// <summary>
/// Represents a osu! authorization token.
/// </summary>
internal sealed class OsuToken : IOsuToken
{
    /// <summary>
    /// Gets when this <see cref="OsuToken" /> was created.
    /// </summary>
    public readonly DateTimeOffset CreatedAt = DateTimeOffset.Now;

    /// <summary>
    /// Gets the type of the token.
    /// </summary>
    public TokenType Type { get; internal set; }

    /// <summary>
    /// Gets the access token.
    /// </summary>
    public string AccessToken { get; internal set; } = null!;

    /// <summary>
    /// Gets the refresh token.
    /// </summary>
    public string RefreshToken { get; internal set; } = null!;

    /// <summary>
    /// Gets whether the token is revoked. Cannot be tracked by the API and is manually updated by the library.
    /// </summary>
    public bool Revoked { get; internal set; }

    /// <summary>
    /// Gets the amount of time until the token expires.
    /// </summary>
    public TimeSpan ExpiresIn => TimeSpan.FromSeconds(ExpiresInSeconds) - (DateTimeOffset.Now - CreatedAt);

    /// <summary>
    /// Gets the amount of time in seconds of the validity of the token.
    /// </summary>
    public long ExpiresInSeconds { get; internal set; }

    /// <summary>
    /// Gets whether the token has expired or not.
    /// </summary>
    public bool HasExpired => ExpiresIn < TimeSpan.Zero || Revoked;

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Type} {AccessToken}";
    }
}