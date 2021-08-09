using System;

namespace OsuSharp.Domain
{
    [Flags]
    public enum BeatmapSearchGeneral
    {
        None = 0,
        Recommended = 1,
        Converts = 2,
        Follows = 4
    }
}
