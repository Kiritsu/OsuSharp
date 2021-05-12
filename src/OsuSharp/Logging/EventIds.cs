using Microsoft.Extensions.Logging;

namespace OsuSharp.Logging
{
    public class EventIds
    {
        public static readonly EventId RestApi = new(1, "RestApi");
        public static readonly EventId RateLimits = new(2, "RateLimits");
        public static readonly EventId Deserialization = new(3, "Deserialization");
    }
}