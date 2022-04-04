using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class BeatmapCover : IBeatmapCover
{
    public string Cover { get; internal set; } = null!;

    public string Cover2x { get; internal set; } = null!;

    public string Card { get; internal set; } = null!;

    public string Card2x { get; internal set; } = null!;

    public string List { get; internal set; } = null!;

    public string List2x { get; internal set; } = null!;

    public string SlimCover { get; internal set; } = null!;

    public string SlimCover2x { get; internal set; } = null!;

    internal BeatmapCover()
    {
            
    }
}