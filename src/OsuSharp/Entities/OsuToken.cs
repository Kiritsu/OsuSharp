using System;
using OsuSharp.Enums;

namespace OsuSharp.Entities
{
    public sealed class OsuToken
    {
        /// <summary>
        ///     Gets the type of the token.
        /// </summary>
        public TokenType Type { get; internal set; }

        /// <summary>
        ///     Gets the access token.
        /// </summary>
        public string AccessToken { get; internal set; }
        
        /// <summary>
        ///     Gets the amount of time until the token has expired.
        /// </summary>
        public TimeSpan ExpiresIn => TimeSpan.FromSeconds(ExpiresInSeconds) - (DateTimeOffset.Now - _creationTime);

        /// <summary>
        ///     Gets whether the token has expired or not.
        /// </summary>
        public bool HasExpired => ExpiresIn < TimeSpan.Zero;

        internal long ExpiresInSeconds { get; set; }

        internal bool IsInternal { get; set; }
        
        private readonly DateTimeOffset _creationTime = DateTimeOffset.Now;
    }
}