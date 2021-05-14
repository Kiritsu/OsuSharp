using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class AchievementEvent : Event, IAchievementEvent
    {
        public object Achievement { get; internal set; }

        public IEventUserModel User { get; internal set; }

        internal AchievementEvent()
        {
            
        }
    }
}