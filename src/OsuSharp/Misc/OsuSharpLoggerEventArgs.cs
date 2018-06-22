using System;
using OsuSharp.Interfaces;

namespace OsuSharp.Misc
{
    public sealed class OsuSharpLoggerEventArgs
    {
        /// <summary>
        ///     Where does the log invoke come from
        /// </summary>
        public string From { get; set; }

        /// <summary>
        ///     Time where the LogMessage was invoked
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        ///     Logged message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     Instance of the IOsuApi
        /// </summary>
        public IOsuApi Instance { get; set; }

        /// <summary>
        ///     Instance of the logger
        /// </summary>
        public IOsuSharpLogger Logger { get; set; }

        /// <summary>
        ///     Level of the logs
        /// </summary>
        public LoggingLevel Level { get; set; }
    }
}