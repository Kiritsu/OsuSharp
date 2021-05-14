using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class BeatmapsetDeleteEvent : Event, IBeatmapsetDeleteEvent
    {
        public IEventBeatmapsetModel Beatmapset { get; internal set; }

        internal BeatmapsetDeleteEvent()
        {
            
        }
    }
}