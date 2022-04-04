using System;
using System.Collections.Generic;

namespace OsuSharp.Interfaces;

public interface ISeasonalBackgrounds : IClientEntity
{
    DateTimeOffset EndsAt { get; }
    IReadOnlyList<ISeasonalBackground> Backgrounds { get; }
}