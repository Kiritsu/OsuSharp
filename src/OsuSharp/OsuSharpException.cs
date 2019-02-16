using System;

namespace OsuSharp
{
    public sealed class OsuSharpException : Exception
    {
        internal OsuSharpException(string message) : base(message)
        {
        }

        internal OsuSharpException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
