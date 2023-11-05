namespace OsuSharp.Interfaces;

public class MultiplayerMatchEventDetails : IMultiplayerMatchEventDetails
{
    public string Type { get; set; } = null!;
    public string? Text { get; set; }

    internal MultiplayerMatchEventDetails()
    {
        
    }
}