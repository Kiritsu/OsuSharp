using System;
using OsuSharp.Attributes;

namespace OsuSharp.Enums
{
    [Flags]
    public enum Mode
    {
        [ModeString("No Mode")] None = 0,

        [ModeString("NF")] NoFail = 1,

        [ModeString("EZ")] Easy = 2,

        [ModeString("NV")] NoVideo = 4,

        [ModeString("HD")] Hidden = 8,

        [ModeString("HR")] HardRock = 16,

        [ModeString("SD")] SuddenDeath = 32,

        [ModeString("DT")] DoubleTime = 64,

        [ModeString("RX")] Relax = 128,

        [ModeString("HT")] HalfTime = 256,

        [ModeString("NC")] Nightcore = 512,

        [ModeString("FL")] Flashlight = 1024,

        [ModeString("Auto")] Autoplay = 2048,

        [ModeString("SO")] SpunOut = 4096,

        [ModeString("AP")] Relax2 = 8192,

        [ModeString("PF")] Perfect = 16384,

        [ModeString("4K")] Key4 = 32768,

        [ModeString("5K")] Key5 = 65536,

        [ModeString("6K")] Key6 = 131072,

        [ModeString("7K")] Key7 = 262144,

        [ModeString("8K")] Key8 = 524288,

        [ModeString("KeyMod")] KeyMod = Key4 | Key5 | Key6 | Key7 | Key8,

        [ModeString("FadeIn")] FadeIn = 1048576,

        [ModeString("Random")] Random = 2097152,

        [ModeString("LastMod")] LastMod = 4194304,

        [ModeString("FreeModAllowed")] FreeModAllowed = NoFail | Easy | Hidden | HardRock | SuddenDeath | Flashlight | FadeIn | Relax | Relax2 | SpunOut | KeyMod,

        [ModeString("9K")] Key9 = 16777216,

        [ModeString("10K")] Key10 = 33554432,

        [ModeString("1K")] Key1 = 67108864,

        [ModeString("3K")] Key3 = 134217728,

        [ModeString("2K")] Key2 = 268435456
    }
}
