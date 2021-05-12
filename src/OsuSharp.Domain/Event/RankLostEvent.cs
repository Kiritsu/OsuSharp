namespace OsuSharp.Domain
{
    public sealed class RankLostEvent : Event
    {
        public GameMode GameMode { get; internal set; }

        public EventBeatmapModel Beatmap { get; internal set; }

        public EventUserModel User { get; internal set; }
    }
}