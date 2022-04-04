using System.Collections.Generic;

namespace OsuSharp.Interfaces;

public interface IUserScores : IClientEntity
{
    IReadOnlyList<IScore> Scores { get; }
}