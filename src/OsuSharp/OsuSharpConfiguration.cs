using System;
using System.Net.Http;
using OsuSharp.Interfaces;
using OsuSharp.Misc;

namespace OsuSharp
{
    public class OsuSharpConfiguration : IOsuSharpConfiguration
    {
        /// <inheritdoc />
        public string ApiKey { get; set; }

        /// <inheritdoc />
        public string ModsSeparator { get; set; }

        /// <inheritdoc />
        public HttpClient CustomHttpClient { get; set; }

        /// <inheritdoc />
        public int MaxRequests { get; set; } = 1200;

        /// <inheritdoc />
        public TimeSpan TimeInterval { get; set; } = TimeSpan.FromMinutes(1);

        /// <inheritdoc />
        public bool ThrowOnMaxRequests { get; set; } = false;

        /// <inheritdoc />
        public LoggingLevel LogLevel { get; set; } = LoggingLevel.Info;
    }
}