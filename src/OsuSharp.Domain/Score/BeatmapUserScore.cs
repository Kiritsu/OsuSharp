using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public class BeatmapUserScore : IBeatmapUserScore
    {
        public int Position { get; internal set; }

        public IScore Score { get; internal set; }

        internal BeatmapUserScore()
        {

        }
    }
}
