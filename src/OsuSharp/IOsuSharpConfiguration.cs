using System;
using System.Net.Http;
using OsuSharp.Misc;

namespace OsuSharp
{
    public interface IOsuSharpConfiguration
    {
        /// <summary>
        /// Your Osu API key (required)
        /// </summary>
        string ApiKey { get; set; }

        /// <summary>
        /// Separator between each mod on Mods property (defaults to empty)
        /// </summary>
        string ModsSeparator { get; set; }

        /// <summary>
        /// Custom http client (facultative)
        /// </summary>
        HttpClient CustomHttpClient { get; set; }

        /// <summary>
        /// Max requests you can perform to the api. (defaults to 1200)
        /// </summary>
        int MaxRequests { get; set; }

        /// <summary>
        /// Time interval in which you can perform your MaxRequests. (defaults to 1 minute)
        /// </summary>
        TimeSpan TimeInterval { get; set; }

        /// <summary>
        /// Does the handler must throw when you're being rate-limited? (defaults to false)
        /// </summary>
        bool ThrowOnMaxRequests { get; set; }

        /// <summary>
        /// Level of the log to fire the event. (defaults to LoggingLevel.Info)
        /// </summary>
        LoggingLevel LogLevel { get; set; }
    }
}
