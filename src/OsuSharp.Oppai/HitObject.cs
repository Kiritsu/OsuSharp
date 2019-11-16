namespace OsuSharp.Oppai
{
    public sealed class HitObject
    {
        /// <summary>
        ///     In milliseconds.
        /// </summary>
        public double Time { get; set; }
        public NoteType Type { get; set; }

        /// <summary>
        ///     Can either be a Circle or a Slider.
        /// </summary>
        public Note Note { get; set; }
        public Vector2 NormPosition { get; set; }
        public double Angle { get; set; }
        public bool IsSingle { get; set; }
        public double DeltaTime { get; set; }
        public double DDistance { get; set; }

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
