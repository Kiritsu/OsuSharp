namespace OsuSharp.Analyzer
{
    public sealed class EntityUpdateEventArgs<T>
    {
        /// <summary>
        ///     Value of the tracked entity before the update.
        /// </summary>
        public T ValueBefore { get; }

        /// <summary>
        ///     Value of the tracked entity after the update.
        /// </summary>
        public T ValueAfter { get; }

        /// <summary>
        ///     Client on which were made the requests.
        /// </summary>
        public OsuClient Client { get; }

        internal EntityUpdateEventArgs(OsuClient client, T before, T after)
        {
            Client = client;
            ValueBefore = before;
            ValueAfter = after;
        }
    }
}
