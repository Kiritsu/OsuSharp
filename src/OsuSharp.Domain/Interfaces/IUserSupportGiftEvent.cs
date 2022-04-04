namespace OsuSharp.Interfaces;

public interface IUserSupportGiftEvent : IEvent
{
    IEventUserModel User { get; }
}