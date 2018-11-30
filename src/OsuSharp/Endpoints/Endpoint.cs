using System;

namespace OsuSharp.Endpoints
{
    public class Endpoint
    {
        public DateTimeOffset InitializedAt { get; } = DateTimeOffset.Now;

        internal Endpoint()
        {

        }
    }
}
