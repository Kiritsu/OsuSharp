using System;
using System.Collections.Generic;

namespace OsuSharp.Interfaces
{
    public interface ISeasonalBackgrounds
    {
        public DateTimeOffset EndsAt { get; }
        public IReadOnlyList<ISeasonalBackground> Backgrounds { get; }
    }

    public interface ISeasonalBackground
    {
        public string Url { get; }
        public IUserCompactBase User { get; }
    }
}
