using System.Collections.Generic;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface IUserRankHistory
{
    GameMode GameMode { get; }
    IReadOnlyList<long> Ranks { get; }
}