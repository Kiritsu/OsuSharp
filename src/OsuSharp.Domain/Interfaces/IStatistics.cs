namespace OsuSharp.Interfaces;

public interface IStatistics
{
    int Count50 { get; }
    int Count100 { get; }
    int Count300 { get; }
    int CountGeki { get; }
    int CountKatu { get; }
    int CountMiss { get; }
}