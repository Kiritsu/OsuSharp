using System;
using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class SeasonalBackgrounds : ISeasonalBackgrounds
    {
        public DateTimeOffset EndsAt { get; internal set; }

        public IReadOnlyList<ISeasonalBackground> Backgrounds { get; internal set; }

        internal SeasonalBackgrounds()
        {

        }
    }

    public sealed class SeasonalBackground : ISeasonalBackground
    {
        public string Url { get; internal set; }

        public IUserCompactBase User { get; internal set; }

        internal SeasonalBackground()
        {

        }
    }
}
