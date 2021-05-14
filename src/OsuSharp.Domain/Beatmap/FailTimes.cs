using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class FailTimes : IFailTimes
    {
        public IReadOnlyList<int> Exit { get; internal set; }

        public IReadOnlyList<int> Fail { get; internal set; }
        
        internal FailTimes()
        {
            
        }
    }
}