using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class BeatmapsetUpdateEvent : Event, IBeatmapsetUpdateEvent
    {
        public IEventBeatmapsetModel Beatmapset { get; internal set; }

        public IEventUserModel User { get; internal set; }

        internal BeatmapsetUpdateEvent()
        {
            
        }
    }
}