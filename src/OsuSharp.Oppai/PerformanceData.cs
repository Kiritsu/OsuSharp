using System.Diagnostics.CodeAnalysis;

namespace OsuSharp.Oppai
{
    [SuppressMessage("Performance", "CA1815",
        Justification = "No need to compare PerformanceData instances.")]
    public struct PerformanceData
    {
        public float Pp { get; }

        public float Stars { get; }

        public float Ar { get; }

        public float Od { get; }

        public float Cs { get; }

        public float Hp { get; }

        public float Accuracy { get; }

        public Mode Mods { get; }

        internal PerformanceData(float pp, float stars, float ar, float od,
            float cs, float hp, float accuracy, Mode mods)
        {
            Pp = pp;
            Stars = stars;
            Ar = ar;
            Od = od;
            Cs = cs;
            Hp = hp;
            Accuracy = accuracy;
            Mods = mods;
        }
    }
}
