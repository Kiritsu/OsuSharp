using System;

namespace OsuSharp.Domain
{
    public sealed class UserAccountHistory
    {
        public long Id { get; internal set; }

        public string Type { get; internal set; }

        public DateTimeOffset TimeStamp { get; internal set; }

        public int Length { get; internal set; }
    }
}