namespace OsuSharp.Oppai
{
    public sealed class HitObject
    {
        /// <summary>
        ///     In milliseconds.
        /// </summary>
        public double Time { get; internal set; }
        public NoteType Type { get; internal set; }

        /// <summary>
        ///     Can either be a Circle or a Slider.
        /// </summary>
        public Note Note { get; internal set; }
        public Vector2 NormPosition { get; internal set; }
        public double Angle { get; internal set; }
        public bool IsSingle { get; internal set; }
        public double DeltaTime { get; internal set; }
        public double DDistance { get; internal set; }

        public readonly double[] Strains = new double[] { 0.0, 0.0 };

        public HitObject()
        {
            Time = 0.0;
            Type = NoteType.Circle;
            Note = null;
            NormPosition = new Vector2();
            Angle = 0.0;
            IsSingle = false;
            DeltaTime = 0.0;
            DDistance = 0.0;
        }
    }
}
