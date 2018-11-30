using OsuSharp.Interfaces;

namespace OsuSharp.Analyzer.Entities
{
    public sealed class UpdateEventArgs<TValue>
    {
        public TValue Before { get; }
        public TValue After { get; }

        public IOsuApi Instance { get; }

        internal UpdateEventArgs(TValue before, TValue after, IOsuApi instance)
        {
            Before = before;
            After = after;
            Instance = instance;
        }
    }
}
