using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class UserRank : IUserRank
{
    public long Global { get; internal set; }

    public long Country { get; internal set; }

    internal UserRank()
    {
            
    }
}