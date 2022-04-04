namespace OsuSharp.Interfaces;

public interface IUserLevel
{
    long Current { get; }
    long Progress { get; }
}