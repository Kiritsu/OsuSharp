namespace OsuSharp.Interfaces;

public interface IBeatmapDifficulty : IClientEntity
{
    IBeatmapDifficultyAttributes Attributes { get; }
}