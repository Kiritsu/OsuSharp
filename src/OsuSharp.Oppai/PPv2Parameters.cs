namespace OsuSharp.Oppai
{
    public sealed class PPv2Parameters
    {
        public ParsedBeatmap Beatmap { get; internal set; }

        public double AimStars { get; internal set; }
        public double SpeedStars { get; internal set; }
        public int MaxCombo { get; internal set; }

        public int CircleCount { get; internal set; }
        public int SliderCount { get; internal set; }
        public int ObjectCount { get; internal set; }

        public float BaseAR { get; internal set; }
        public float BaseOD { get; internal set; }

        public int GameMode { get; internal set; }

        public int Mods { get; internal set; }

        public int Combo { get; internal set; }

        public int Count300 { get; internal set; }
        public int Count100 { get; internal set; }
        public int Count50 { get; internal set; }
        public int CountMiss { get; internal set; }

        public int ScoreVersion { get; internal set; }

        public PPv2Parameters()
        {
            Beatmap = null;
            AimStars = 0.0;
            SpeedStars = 0.0;
            MaxCombo = 0;
            SliderCount = 0;
            CircleCount = 0;
            ObjectCount = 0;
            BaseAR = 5.0F;
            BaseOD = 5.0F;
            GameMode = OppaiUtilities.MODE_STD;
            Mods = OppaiUtilities.MODS_NOMOD;
            Combo = -1;
            Count300 = -1;
            Count100 = 0;
            Count50 = 0;
            CountMiss = 0;
            ScoreVersion = 1;
        }
    }
}
