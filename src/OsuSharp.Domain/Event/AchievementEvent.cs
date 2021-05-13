namespace OsuSharp.Domain
{
    public sealed class AchievementEvent : Event
    {
        public object Achievement { get; internal set; }

        public EventUserModel User { get; internal set; }

        internal AchievementEvent()
        {
            
        }
    }
}