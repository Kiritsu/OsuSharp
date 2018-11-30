using System;
using System.Threading;
using OsuSharp.Enums;
using OsuSharp.Interfaces;
using OsuSharp.Misc;

namespace OsuSharp
{
    public sealed class OsuSharpLogger : IOsuSharpLogger
    {
        private OsuApi Instance { get; }
        private SemaphoreSlim Semaphore { get; }
        private LoggingLevel Level { get; }

        public OsuSharpLogger(OsuApi instance, LoggingLevel level)
        {
            Instance = instance;
            Level = level;
            Semaphore = new SemaphoreSlim(1);
        }

        /// <summary>
        ///     Event that fires when IOsuSharpLogger#LogMessage is invoked.
        /// </summary>
        public event EventHandler<OsuSharpLoggerEventArgs> LogMessageReceived;

        /// <summary>
        ///     Invoke the event to log a new message.
        /// </summary>
        /// <param name="level">Level of the message to log</param>
        /// <param name="from">Where does the log come from</param>
        /// <param name="message">Content to log</param>
        /// <param name="time">Time when it happened</param>
        public void LogMessage(LoggingLevel level, string from, string message, DateTime time)
        {
            if (level <= Level)
            {
                LogMessageReceived?.Invoke(this, new OsuSharpLoggerEventArgs
                {
                    Instance = Instance,
                    Message = message,
                    Logger = this,
                    Time = time,
                    From = from,
                    Level = level
                });
            }
        }

        /// <summary>
        ///     Print a colored message to the console depending on the LoggingLevel
        /// </summary>
        /// <param name="level">Level of the log</param>
        /// <param name="from">From where does the message come from</param>
        /// <param name="message">Content to be logged</param>
        /// <param name="time">Time when it happened</param>
        public void Print(LoggingLevel level, string from, string message, DateTime time)
        {
            Semaphore.Wait();

            try
            {
                switch (level)
                {
                    default:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case LoggingLevel.Debug:
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        break;
                    case LoggingLevel.Info:
                        Console.ForegroundColor = ConsoleColor.White;
                        break;
                    case LoggingLevel.Warning:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case LoggingLevel.Error:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                }

                Console.Write($"[{time:dd/MM/yyyy HH:mm:ss}] [{from}] [{level}] ");
                Console.ResetColor();
                Console.WriteLine(message);
            }
            finally
            {
                Semaphore.Release();
            }
        }
    }
}