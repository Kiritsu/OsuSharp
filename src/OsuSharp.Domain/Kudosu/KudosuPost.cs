using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class KudosuPost : IKudosuPost
{
    public string Url { get; internal set; } = null!;

    public string Title { get; internal set; } = null!;

    internal KudosuPost()
    {
            
    }
}