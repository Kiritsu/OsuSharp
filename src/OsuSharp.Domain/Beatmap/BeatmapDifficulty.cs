namespace OsuSharp.Interfaces;

public class BeatmapDifficulty : IBeatmapDifficulty
{
    public IOsuClient Client { get; internal set; } = null!;
    public IBeatmapDifficultyAttributes Attributes { get; internal set; } = null!;

    internal BeatmapDifficulty()
    {
        
    }
}