using Microsoft.Extensions.Logging;

namespace OsuSharp.Logging
{
    public class EventIds
    {
        public static EventId RestApi = new EventId(1, "RestApi");
        public static EventId RateLimits = new EventId(2, "RateLimits");
    }
}