namespace OsuSharp.Interfaces
{
    public interface IBeatmapUserScore
    {
        int Position { get; }
        IScore Score { get; }
    }
}
