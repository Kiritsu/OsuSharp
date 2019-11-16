namespace OsuSharp.Oppai
{
    public sealed class Timing
    {
        /// <summary>
        ///     In milliseconds.
        /// </summary>
        public double Time { get; internal set; }

        public double MsPerBeat { get; internal set; }

        /// <summary>
        ///     When false, <see cref="MsPerBeat"/> is "-100 * BpmMultiplier".
        /// </summary>
        public bool Change { get; internal set; }

        public Timing()
        {
            Time = 0.0;
            MsPerBeat = -100;
            Change = false;
        }
    }
}
