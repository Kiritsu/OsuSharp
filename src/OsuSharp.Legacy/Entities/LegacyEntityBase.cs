namespace OsuSharp.Legacy.Entities;

public abstract class LegacyEntityBase
{
    public LegacyOsuClient Client { get; internal set; }

    internal LegacyEntityBase() { }
}