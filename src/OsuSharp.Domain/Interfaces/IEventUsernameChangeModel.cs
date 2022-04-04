namespace OsuSharp.Interfaces;

public interface IEventUsernameChangeModel : IEventUserModel
{
    string PreviousUsername { get; }
}