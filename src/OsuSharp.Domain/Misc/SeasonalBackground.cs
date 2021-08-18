using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class SeasonalBackground : ISeasonalBackground
    {
        public string Url { get; internal set; }

        public IUserCompactBase User { get; internal set; }

        internal SeasonalBackground()
        {

        }
    }
}
