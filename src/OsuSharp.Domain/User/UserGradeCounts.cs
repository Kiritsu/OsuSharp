using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class UserGradeCounts : IUserGradeCounts
{
    public long SS { get; internal set; }

    public long? SSH { get; internal set; }

    public long S { get; internal set; }

    public long? SH { get; internal set; }

    public long A { get; internal set; }

    internal UserGradeCounts()
    {
            
    }
}