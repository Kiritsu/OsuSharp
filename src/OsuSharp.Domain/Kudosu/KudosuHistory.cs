using System;

namespace OsuSharp.Domain
{
    public sealed class KudosuHistory 
    {
        public long Id { get; internal set; }

        public KudosuAction Action { get; internal set; }

        public long Amount { get; internal set; }

        //todo: make enum
        public string Model { get; internal set; }

        public DateTimeOffset CreatedAt { get; internal set; }

        public KudosuGiver Giver { get; internal set; }

        public KudosuPost Post { get; internal set; }
    }
}
