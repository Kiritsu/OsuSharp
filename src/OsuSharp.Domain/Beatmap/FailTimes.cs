using System.Collections.Generic;

namespace OsuSharp.Domain
{
    public sealed class FailTimes
    {
        public IReadOnlyList<int> Exit { get; internal set; }

        public IReadOnlyList<int> Fail { get; internal set; }
    }
}