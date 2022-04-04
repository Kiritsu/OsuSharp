using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public class UserScores : IUserScores
{
    public IOsuClient Client { get; internal set; } = null!;

    public IReadOnlyList<IScore> Scores { get; internal set; } = null!;

    internal UserScores()
    {

    }
}