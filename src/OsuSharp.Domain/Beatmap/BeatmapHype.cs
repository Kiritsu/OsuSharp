using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class BeatmapHype : IBeatmapHype
{
    public int CurrentHype { get; internal set; }

    public int RequiredHype { get; internal set; }
        
    internal BeatmapHype()
    {
            
    }
}