using System;

namespace OsuSharp.Legacy;

public sealed class OsuSharpLogger
{
    /// <summary>
    ///     Events that will fire each time <see cref="LogMessage(string)"/> has been called.
    /// </summary>
    public event Action<string> LogMessageReceived;

    internal void LogMessage(string message)
    {
        LogMessageReceived?.Invoke(message);
    }
}