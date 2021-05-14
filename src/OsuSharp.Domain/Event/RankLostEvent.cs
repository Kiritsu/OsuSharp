using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class RankLostEvent : Event, IRankLostEvent
    {
        public GameMode GameMode { get; internal set; }

        public IEventBeatmapModel Beatmap { get; internal set; }

        public IEventUserModel User { get; internal set; }

        internal RankLostEvent()
        {
            
        }
    }
}