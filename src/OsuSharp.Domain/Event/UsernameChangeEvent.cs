using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class UsernameChangeEvent : Event, IUsernameChangeEvent
{
    public IEventUsernameChangeModel User { get; internal set; } = null!;

    internal UsernameChangeEvent()
    {
            
    }
}