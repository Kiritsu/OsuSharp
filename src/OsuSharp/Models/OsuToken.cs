using System;
using OsuSharp.Domain;

namespace OsuSharp.Models
{
    /// <summary>
    /// Represents a osu token.
    /// </summary>
    public sealed class OsuToken
    {
        /// <summary>
        ///     Gets when this <see cref="OsuToken" /> was created.
        /// </summary>
        public readonly DateTimeOffset CreatedAt = DateTimeOffset.Now;

        /// <summary>
        ///     Gets the type of the token.
        /// </summary>
        public TokenType Type { get; internal init; }

        /// <summary>
        ///     Gets the access token.
        /// </summary>
        public string AccessToken { get; internal init; }

        /// <summary>
        ///     Gets the refresh token.
        /// </summary>
        public string RefreshToken { get; internal init; }

        /// <summary>
        ///     Gets whether the token is revoked. Cannot be tracked by the API and is manually updated by the library.
        /// </summary>
        public bool Revoked { get; internal set; }

        /// <summary>
        ///     Gets the amount of time until the token has expired.
        /// </summary>
        public TimeSpan ExpiresIn => TimeSpan.FromSeconds(ExpiresInSeconds) - (DateTimeOffset.Now - CreatedAt);

        internal long ExpiresInSeconds { get; set; }

        /// <summary>
        ///     Gets whether the token has expired or not.
        /// </summary>
        public bool HasExpired => ExpiresIn < TimeSpan.Zero || Revoked;

        /// <inheritdoc/>
        public override string ToString()
        {
            return $"{Type} {AccessToken}";
        }
    }
}