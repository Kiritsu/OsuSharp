using System;
using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class SeasonalBackgrounds : ISeasonalBackgrounds
    {
        public DateTimeOffset EndsAt { get; internal set; }

        public IReadOnlyList<ISeasonalBackground> Backgrounds { get; internal set; }

        public IOsuClient Client { get; internal set; }

        internal SeasonalBackgrounds()
        {

        }
    }
}
