namespace OsuSharp.Domain
{
    public sealed class BeatmapsetReviveEvent : Event
    {
        public EventBeatmapsetModel Beatmapset { get; internal set; }

        public EventUserModel User { get; internal set; }
    }
}