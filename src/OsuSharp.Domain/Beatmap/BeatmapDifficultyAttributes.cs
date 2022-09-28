namespace OsuSharp.Interfaces;

public class BeatmapDifficultyAttributes : IBeatmapDifficultyAttributes
{
    public int MaxCombo { get; internal set; }
    public double StarRating { get; internal set; }
    public double? AimDifficulty { get; internal set; }
    public double? ApproachRate { get; internal set; }
    public double? FlashlightDifficulty { get; internal set; }
    public double? OverallDifficulty { get; internal set; }
    public double? SliderFactor { get; internal set; }
    public double? SpeedDifficulty { get; internal set; }
    public double? StaminaDifficulty { get; internal set; }
    public double? RhythmDifficulty { get; internal set; }
    public double? ColourDifficulty { get; internal set; }
    public double? GreatHitWindow { get; internal set; }
    public double? ScoreMultiplier { get; internal set; }

    internal BeatmapDifficultyAttributes()
    {
        
    }
}