using System.Collections.Generic;

namespace OsuSharp.Interfaces;

public interface IFailTimes
{
    IReadOnlyList<int> Exit { get; }
    IReadOnlyList<int> Fail { get; }
}