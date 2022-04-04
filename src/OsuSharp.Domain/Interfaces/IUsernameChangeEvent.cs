namespace OsuSharp.Interfaces;

public interface IUsernameChangeEvent : IEvent
{
    IEventUsernameChangeModel User { get; }
}