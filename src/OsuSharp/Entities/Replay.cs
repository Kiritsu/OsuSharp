namespace OsuSharp.Entities
{
    public sealed class Replay
    {
        /// <summary>
        ///     The type of encoding used to encode <see cref="Content"/>
        /// </summary>
        public string Encoding { get; internal set; }

        /// <summary>
        ///     Content of the replay.
        /// </summary>
        public string Content { get; internal set; }
    }
}
