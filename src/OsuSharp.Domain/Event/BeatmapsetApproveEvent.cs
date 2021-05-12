namespace OsuSharp.Domain
{
    public sealed class BeatmapsetApproveEvent : Event
    {
        public RankStatus Approval { get; internal set; }

        public EventBeatmapsetModel Beatmapset { get; internal set; }

        public EventUserModel User { get; internal set; }
    }
}