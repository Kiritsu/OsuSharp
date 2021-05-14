using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class BeatmapsetApproveEvent : Event, IBeatmapsetApproveEvent
    {
        public RankStatus Approval { get; internal set; }

        public IEventBeatmapsetModel Beatmapset { get; internal set; }

        public IEventUserModel User { get; internal set; }

        internal BeatmapsetApproveEvent()
        {
            
        }
    }
}