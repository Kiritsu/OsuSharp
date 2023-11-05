namespace OsuSharp.Interfaces;

public interface IMultiplayerHistoryParams
{
    int Limit { get; set; }
    string Sort { get; set; }
}