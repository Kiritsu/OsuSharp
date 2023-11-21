namespace OsuSharp.Interfaces;

public interface IMultiplayerMatchEventDetails
{
    string Type { get; }
    string? Text { get; }
}