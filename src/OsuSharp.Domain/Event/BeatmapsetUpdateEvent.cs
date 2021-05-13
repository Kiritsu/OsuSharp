namespace OsuSharp.Domain
{
    public sealed class BeatmapsetUpdateEvent : Event
    {
        public EventBeatmapsetModel Beatmapset { get; internal set; }

        public EventUserModel User { get; internal set; }

        internal BeatmapsetUpdateEvent()
        {
            
        }
    }
}