namespace OsuSharp.Domain
{
    public sealed class BeatmapPlaycountEvent : Event
    {
        public int Count { get; internal set; }

        public EventBeatmapModel Beatmap { get; internal set; }

        internal BeatmapPlaycountEvent()
        {
            
        }
    }
}