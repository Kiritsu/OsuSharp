using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public class EventUserModel : IEventUserModel
{
    public string Username { get; internal set; } = null!;

    public string Url { get; internal set; } = null!;

    internal EventUserModel()
    {
            
    }
}