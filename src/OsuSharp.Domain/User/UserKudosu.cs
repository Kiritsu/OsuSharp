using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class UserKudosu : IUserKudosu
{
    public long Total { get; internal set; }

    public long Available { get; internal set; }

    internal UserKudosu()
    {
            
    }
}