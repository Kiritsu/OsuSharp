using System;

namespace OsuSharp.Domain
{
    public abstract class Event
    {
        public DateTimeOffset CreatedAt { get; internal set; }

        public long Id { get; internal set; }

        public EventType Type { get; internal set; }
    }
}