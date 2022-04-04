using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class EventBeatmapModel : IEventBeatmapModel
{
    public string Title { get; internal set; } = null!;

    public string Url { get; internal set; } = null!;

    internal EventBeatmapModel()
    {
            
    }
}