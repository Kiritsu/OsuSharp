using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public class UserScores : IUserScores
{
    public IOsuClient Client { get; internal set; }
    
    public IReadOnlyList<IScore> Scores { get; internal set; }
    
    internal UserScores()
    {
            
    }
}