using Microsoft.Extensions.Logging;

namespace OsuSharp.Logging
{
    public class EventIds
    {
        public static EventId RestApi = new(1, "RestApi");
        public static EventId RateLimits = new(2, "RateLimits");
    }
}