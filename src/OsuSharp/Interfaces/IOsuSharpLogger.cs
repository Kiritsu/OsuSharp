using System;
using OsuSharp.Enums;
using OsuSharp.Misc;

namespace OsuSharp.Interfaces
{
    public interface IOsuSharpLogger
    {
        /// <summary>
        ///     Event that fires when IOsuSharpLogger#LogMessage is invoked.
        /// </summary>
        event EventHandler<OsuSharpLoggerEventArgs> LogMessageReceived;

        /// <summary>
        ///     Invoke the event to log a new message.
        /// </summary>
        /// <param name="level">Level of the message to log</param>
        /// <param name="from">Where does the log come from</param>
        /// <param name="message">Content to log</param>
        /// <param name="time">Time when it happened</param>
        void LogMessage(LoggingLevel level, string from, string message, DateTime time);

        /// <summary>
        ///     Print a colored message to the console depending on the LoggingLevel
        /// </summary>
        /// <param name="level">Level of the log</param>
        /// <param name="from">From where does the message come from</param>
        /// <param name="message">Content to be logged</param>
        /// <param name="time">Time when it happened</param>
        void Print(LoggingLevel level, string from, string message, DateTime time);
    }
}