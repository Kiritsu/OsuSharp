namespace OsuSharp.Oppai
{
    public sealed class Timing
    {
        /// <summary>
        ///     In milliseconds.
        /// </summary>
        public double Time { get; set; }

        public double MsPerBeat { get; set; }

        /// <summary>
        ///     When false, <see cref="MsPerBeat"/> is "-100 * BpmMultiplier".
        /// </summary>
        public bool Change { get; set; }

        public Timing()
        {
            Time = 0.0;
            MsPerBeat = -100;
            Change = false;
        }
    }
}
