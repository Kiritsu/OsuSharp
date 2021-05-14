using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class UserSupportGiftEvent : Event, IUserSupportGiftEvent
    {
        public IEventUserModel User { get; internal set; }

        internal UserSupportGiftEvent()
        {
            
        }
    }
}