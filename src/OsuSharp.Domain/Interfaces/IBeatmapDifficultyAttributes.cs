namespace OsuSharp.Interfaces;

public interface IBeatmapDifficultyAttributes
{
    int MaxCombo { get; }
    double StarRating { get; }
    double? AimDifficulty { get; }
    double? ApproachRate { get; }
    double? FlashlightDifficulty { get; }
    double? OverallDifficulty { get; }
    double? SliderFactor { get; }
    double? SpeedDifficulty { get; }
    double? StaminaDifficulty { get; }
    double? RhythmDifficulty { get; }
    double? ColourDifficulty { get; }
    double? GreatHitWindow { get; }
    double? ScoreMultiplier { get; }
}