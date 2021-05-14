using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class BeatmapsetUploadEvent : Event, IBeatmapsetUploadEvent
    {
        public IEventBeatmapsetModel Beatmapset { get; internal set; }

        public IEventUserModel User { get; internal set; }

        internal BeatmapsetUploadEvent()
        {
            
        }
    }
}