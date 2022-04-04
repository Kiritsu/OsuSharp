namespace OsuSharp.Interfaces;

public interface IUserSupportAgainEvent : IEvent
{
    IEventUserModel User { get; }
}