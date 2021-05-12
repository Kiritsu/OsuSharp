using System.Collections.Generic;

namespace OsuSharp.Domain
{
    public sealed class UserRankHistory
    {
        public GameMode GameMode { get; internal set; }

        public IReadOnlyCollection<long> Ranks { get; internal set; }
    }
}