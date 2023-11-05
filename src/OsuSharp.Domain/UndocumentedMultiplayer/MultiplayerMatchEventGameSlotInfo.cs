using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public class MultiplayerMatchEventGameSlotInfo : IMultiplayerMatchEventGameSlotInfo
{
    public int Slot { get; set; }
    public TeamColor Team { get; set; }
    public bool Pass { get; set; }

    internal MultiplayerMatchEventGameSlotInfo()
    {
        
    }
}