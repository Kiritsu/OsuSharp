using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class BeatmapNomination : IBeatmapNomination
{
    public int Current { get; internal set; }

    public int Required { get; internal set; }
        
    internal BeatmapNomination()
    {
            
    }
}