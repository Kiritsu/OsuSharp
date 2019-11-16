namespace OsuSharp.Oppai
{
    public sealed class BeatmapStats
    {
        public float AR { get; internal set; }
        public float OD { get; internal set; }
        public float CS { get; internal set; }
        public float HP { get; internal set; }

        /// <summary>
        ///     Speed multipler / Music rate.
        /// </summary>
        public float Speed { get; internal set; }
    }
}
