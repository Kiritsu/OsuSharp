using System;

namespace OsuSharp.Domain
{
    [Flags]
    public enum BeatmapRank
    {
        None = 0,
        XH = 1,
        X = 2,
        SH = 4,
        S = 8,
        A = 16,
        B = 32,
        C = 64,
        D = 128
    }
}
