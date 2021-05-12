namespace OsuSharp.Domain
{
    public sealed class BeatmapsetDeleteEvent : Event
    {
        public EventBeatmapsetModel Beatmapset { get; internal set; }
    }
}
