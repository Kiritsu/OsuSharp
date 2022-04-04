using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class UserRankHistory : IUserRankHistory
{
    public GameMode GameMode { get; internal set; }

    public IReadOnlyList<long> Ranks { get; internal set; } = null!;

    internal UserRankHistory()
    {
            
    }
}