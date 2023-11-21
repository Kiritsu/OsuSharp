namespace OsuSharp.Interfaces;

public interface IMultiplayerHistoryParams
{
    int Limit { get; }
    string Sort { get; }
}