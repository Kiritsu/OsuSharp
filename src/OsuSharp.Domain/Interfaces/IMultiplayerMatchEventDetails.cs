namespace OsuSharp.Interfaces;

public interface IMultiplayerMatchEventDetails
{
    string Type { get; set; }
    string? Text { get; set; }
}