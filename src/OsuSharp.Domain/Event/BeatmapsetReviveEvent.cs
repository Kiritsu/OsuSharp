using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class BeatmapsetReviveEvent : Event, IBeatmapsetReviveEvent
    {
        public IEventBeatmapsetModel Beatmapset { get; internal set; }

        public IEventUserModel User { get; internal set; }

        internal BeatmapsetReviveEvent()
        {
            
        }
    }
}