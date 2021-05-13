namespace OsuSharp.Domain
{
    public sealed class RankEvent : Event
    {
        public string ScoreRank { get; internal set; }

        public long Rank { get; internal set; }

        public GameMode GameMode { get; internal set; }

        public EventBeatmapModel Beatmap { get; internal set; }

        public EventUserModel User { get; internal set; }

        internal RankEvent()
        {
            
        }
    }
}