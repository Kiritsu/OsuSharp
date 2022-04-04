using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class BeatmapUserScore : IBeatmapUserScore
{
    public int Position { get; internal set; }

    public IScore Score { get; internal set; } = null!;

    internal BeatmapUserScore()
    {

    }
}