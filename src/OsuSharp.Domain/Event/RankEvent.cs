using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class RankEvent : Event, IRankEvent
    {
        public string ScoreRank { get; internal set; }

        public long Rank { get; internal set; }

        public GameMode GameMode { get; internal set; }

        public IEventBeatmapModel Beatmap { get; internal set; }

        public IEventUserModel User { get; internal set; }

        internal RankEvent()
        {
            
        }
    }
}