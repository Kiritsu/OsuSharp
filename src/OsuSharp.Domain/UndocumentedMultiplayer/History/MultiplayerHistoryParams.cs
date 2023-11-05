namespace OsuSharp.Interfaces;

public class MultiplayerHistoryParams : IMultiplayerHistoryParams
{
    public int Limit { get; set; }
    public string Sort { get; set; } = null!;

    internal MultiplayerHistoryParams()
    {
        
    }
}