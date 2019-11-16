using System;

namespace OsuSharp.Oppai
{
    [Flags]
    public enum NoteType
    {
        Circle = 1 << 0,
        Slider = 1 << 1,
        Spinner = 1 << 3
    }
}
