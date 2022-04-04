using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class FailTimes : IFailTimes
{
    public IReadOnlyList<int> Exit { get; internal set; } = null!;

    public IReadOnlyList<int> Fail { get; internal set; } = null!;

    internal FailTimes()
    {
            
    }
}