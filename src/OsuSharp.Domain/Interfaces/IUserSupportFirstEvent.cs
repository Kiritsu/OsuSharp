namespace OsuSharp.Interfaces;

public interface IUserSupportFirstEvent : IEvent
{
    IEventUserModel User { get; }
}