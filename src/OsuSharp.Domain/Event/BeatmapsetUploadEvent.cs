using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class BeatmapsetUploadEvent : Event, IBeatmapsetUploadEvent
{
    public IEventBeatmapsetModel Beatmapset { get; internal set; } = null!;

    public IEventUserModel User { get; internal set; } = null!;

    internal BeatmapsetUploadEvent()
    {
            
    }
}