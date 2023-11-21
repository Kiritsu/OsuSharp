using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface IMultiplayerMatchEventGameSlotInfo
{
    int Slot { get; }
    TeamColor Team { get; }
    bool Pass { get; }
}