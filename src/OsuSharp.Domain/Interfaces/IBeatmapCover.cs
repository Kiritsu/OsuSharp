namespace OsuSharp.Interfaces;

/// <summary>
/// Defines a beatmap cover object.
/// </summary>
public interface IBeatmapCover
{
    string Cover { get; }
    string Cover2x { get; }
    string Card { get; }
    string Card2x { get; }
    string List { get; }
    string List2x { get; }
    string SlimCover { get; }
    string SlimCover2x { get; }
}