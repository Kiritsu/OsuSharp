using System;
using OsuSharp.Misc;

namespace OsuSharp
{
    public class OsuSharpLogger : IOsuSharpLogger
    {
        private OsuApi Instance { get; }

        private LoggingLevel Level { get; }

        public OsuSharpLogger(OsuApi instance, LoggingLevel level)
        {
            Instance = instance;
            Level = level;
        }

        /// <inheritdoc />
        public event EventHandler<OsuSharpLoggerEventArgs> LogMessageReceived;

        /// <inheritdoc />
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

        /// <inheritdoc />
        public void Print(LoggingLevel level, string from, string message, DateTime time)
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
    }
}
