using System;
using System.Net.Http;
using OsuSharp.Enums;
using OsuSharp.Interfaces;

namespace OsuSharp
{
    public sealed class OsuSharpConfiguration : IOsuSharpConfiguration
    {
        /// <summary>
        ///     Your osu!API key (required)
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        ///     Separator between each mod on Mods property (defaults to empty)
        /// </summary>
        public string ModsSeparator { get; set; }

        /// <summary>
        ///     Custom http client (facultative)
        /// </summary>
        public HttpClient CustomHttpClient { get; set; }

        /// <summary>
        ///     Max requests you can perform to the api. (defaults to 1200)
        /// </summary>
        public int MaxRequests { get; set; } = 1200;

        /// <summary>
        ///     Time interval in which you can perform your MaxRequests. (defaults to 1 minute)
        /// </summary>
        public TimeSpan TimeInterval { get; set; } = TimeSpan.FromMinutes(1);

        /// <summary>
        ///     Does the handler must throw when you're being rate-limited? (defaults to false)
        /// </summary>
        public bool ThrowOnMaxRequests { get; set; } = false;

        /// <summary>
        ///     Level of the log to fire the event. (defaults to LoggingLevel.Info)
        /// </summary>
        public LoggingLevel LogLevel { get; set; } = LoggingLevel.Info;
    }
}