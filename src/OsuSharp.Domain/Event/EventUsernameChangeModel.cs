using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class EventUsernameChangeModel : EventUserModel, IEventUsernameChangeModel
{
    public string PreviousUsername { get; internal set; } = null!;

    internal EventUsernameChangeModel()
    {
            
    }
}