using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class UserLevel : IUserLevel
{
    public long Current { get; internal set; }

    public long Progress { get; internal set; }

    internal UserLevel()
    {
            
    }
}