using System;
using Microsoft.Extensions.Logging;

namespace OsuSharp.Logging
{
    public sealed class DefaultLogger : ILogger<OsuClient>
    {
        private static readonly object Lock = new();

        private readonly OsuClientConfiguration _config;

        public DefaultLogger(OsuClientConfiguration config)
        {
            _config = config;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            lock (Lock)
            {
                Console.Write($"[{DateTimeOffset.Now.ToString(_config.LoggerDateTimeOffsetFormat)}] [{eventId.Id}:{eventId.Name}] ");

                switch (logLevel)
                {
                    case LogLevel.Trace:
                    case LogLevel.Debug:
                        Console.ForegroundColor = ConsoleColor.Green;
                        break;
                    case LogLevel.Information:
                        Console.ForegroundColor = ConsoleColor.Gray;
                        break;
                    case LogLevel.Warning:
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        break;
                    case LogLevel.Error:
                        Console.ForegroundColor = ConsoleColor.Red;
                        break;
                    case LogLevel.Critical:
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        break;
                }
                
                Console.Write($"[{logLevel}] ");
                Console.ResetColor();
                
                Console.WriteLine(formatter(state, exception));

                if (exception != null)
                {
                    Console.WriteLine(_config.LoggerExceptionFormat(exception));
                }
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            lock (Lock)
            {
                return logLevel >= _config.LoggingLevel;
            }
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            throw new InvalidOperationException();
        }
    }
}