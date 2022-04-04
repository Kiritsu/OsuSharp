using System;

namespace OsuSharp.Domain;

/// <summary>
/// Represents the differents mods applicable to a score on a beatmap.
/// </summary>
[Flags]
public enum Mods
{
    [ModsString("No Mode")]
    None = 0,

    [ModsString("NF")]
    NoFail = 1,

    [ModsString("EZ")]
    Easy = 2,

    [ModsString("NV")]
    NoVideo = 4,

    [ModsString("HD")]
    Hidden = 8,

    [ModsString("HR")]
    HardRock = 16,

    [ModsString("SD")]
    SuddenDeath = 32,

    [ModsString("DT")]
    DoubleTime = 64,

    [ModsString("RX")]
    Relax = 128,

    [ModsString("HT")]
    HalfTime = 256,

    [ModsString("NC")]
    Nightcore = 512,

    [ModsString("FL")]
    Flashlight = 1024,

    [ModsString("Auto")]
    Autoplay = 2048,

    [ModsString("SO")]
    SpunOut = 4096,

    [ModsString("AP")]
    AutoPilot = 8192,

    [ModsString("PF")]
    Perfect = 16384,

    [ModsString("4K")]
    Key4 = 32768,

    [ModsString("5K")]
    Key5 = 65536,

    [ModsString("6K")]
    Key6 = 131072,

    [ModsString("7K")]
    Key7 = 262144,

    [ModsString("8K")]
    Key8 = 524288,

    [ModsString("FadeIn")]
    FadeIn = 1048576,

    [ModsString("Random")]
    Random = 2097152,

    [ModsString("Cinema")]
    Cinema = 4194304,

    [ModsString("9K")]
    Key9 = 16777216,

    [ModsString("Coop")]
    KeyCoop = 33554432,

    [ModsString("1K")]
    Key1 = 67108864,

    [ModsString("3K")]
    Key3 = 134217728,

    [ModsString("2K")]
    Key2 = 268435456,

    [ModsString("V2")]
    ScoreV2 = 536870912,

    [ModsString("LM")]
    LastMod = 1073741824,

    [ModsString("KeyMod")]
    KeyMod = Key1 | Key2 | Key3 | Key4 | Key5 | Key6 | Key7 | Key8 | Key9 | KeyCoop,

    [ModsString("FreeModAllowed")]
    FreeModAllowed = NoFail | Easy | Hidden | HardRock | SuddenDeath | Flashlight |
                     FadeIn | Relax | AutoPilot | SpunOut | KeyMod,

    [ModsString("ScoreIncreaseMods")]
    ScoreIncreaseMods = Hidden | HardRock | DoubleTime | Flashlight | FadeIn
}