using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface IMultiplayerMatchEventGameSlotInfo
{
    int Slot { get; set; }
    TeamColor Team { get; set; }
    bool Pass { get; set; }
}