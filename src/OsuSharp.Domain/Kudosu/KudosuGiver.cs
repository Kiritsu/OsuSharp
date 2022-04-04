using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class KudosuGiver : IKudosuGiver
{
    public string Url { get; internal set; } = null!;

    public string Username { get; internal set; } = null!;

    internal KudosuGiver()
    {
            
    }
}