using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class UserSupportFirstEvent : Event, IUserSupportFirstEvent
    {
        public IEventUserModel User { get; internal set; }

        internal UserSupportFirstEvent()
        {
            
        }
    }
}