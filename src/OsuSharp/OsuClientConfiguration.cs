using System;
using Microsoft.Extensions.Logging;
using OsuSharp.Logging;

namespace OsuSharp
{
    public sealed class OsuClientConfiguration
    {
        /// <summary>
        ///     Gets or sets the given client ID after registering your application.
        /// </summary>
        public long ClientId { get; set; }

        /// <summary>
        ///     Gets or sets the given client secret after registering your application.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        ///     Gets or sets whether to wait or throw on rate-limits. Defaults to false.
        /// </summary>
        public bool ThrowOnRateLimits { get; set; } = false;

        /// <summary>
        ///     Gets or sets the logger to use. Defaults to <see cref="DefaultLogger" /> when null.
        /// </summary>
        public ILogger<OsuClient> Logger { get; set; }

        /// <summary>
        ///     Gets or sets the minimum logging level for the <see cref="ILogger{OsuClient}" />.
        /// </summary>
        public LogLevel LogLevel { get; set; } = LogLevel.Debug;

        /// <summary>
        ///     Gets or sets the logger <see cref="DateTimeOffset" /> format.
        ///     This applies for the default logger only. Defaults to Long General.
        /// </summary>
        public string LoggerDateTimeOffsetFormat { get; set; } = "G";

        /// <summary>
        ///     Gets or sets the logger <see cref="Exception" /> printing format.
        ///     This applies for the default logger only. Defaults to <see cref="Exception" /> ToString.
        /// </summary>
        public Func<Exception, string> LoggerExceptionFormat { get; set; } = e => e.ToString();
    }
}