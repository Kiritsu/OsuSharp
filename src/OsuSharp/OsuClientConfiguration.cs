namespace OsuSharp
{
    public sealed class OsuClientConfiguration
    {
        /// <summary>
        ///     Gets or sets the given client ID after registering your application.
        /// </summary>
        public uint ClientId { get; set; }
        
        /// <summary>
        ///     Gets or sets the given client secret after registering your application.
        /// </summary>
        public string ClientSecret { get; set; }
        
        /// <summary>
        ///     Gets or sets whether to wait or thrown on rate-limits.
        /// </summary>
        public bool ThrowOnRateLimits { get; set; }
    }
}