using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class SeasonalBackground : ISeasonalBackground
{
    public string Url { get; internal set; } = null!;

    public IUserCompact User { get; internal set; } = null!;

    internal SeasonalBackground()
    {

    }
}