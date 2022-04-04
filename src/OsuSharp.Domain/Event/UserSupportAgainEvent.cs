using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class UserSupportAgainEvent : Event, IUserSupportAgainEvent
{
    public IEventUserModel User { get; internal set; } = null!;

    internal UserSupportAgainEvent()
    {
            
    }
}