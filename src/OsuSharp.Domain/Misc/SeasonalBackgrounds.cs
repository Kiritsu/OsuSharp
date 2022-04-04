using System;
using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class SeasonalBackgrounds : ISeasonalBackgrounds
{
    public DateTimeOffset EndsAt { get; internal set; }

    public IReadOnlyList<ISeasonalBackground> Backgrounds { get; internal set; } = null!;

    public IOsuClient Client { get; internal set; } = null!;

    internal SeasonalBackgrounds()
    {

    }
}