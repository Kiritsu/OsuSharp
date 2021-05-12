using System.Diagnostics.CodeAnalysis;

namespace OsuSharp.Domain
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public sealed class UserGradeCounts
    {
        public long SS { get; internal set; }

        public long SSH { get; internal set; }

        public long S { get; internal set; }

        public long SH { get; internal set; }

        public long A { get; internal set; }
    }
}
