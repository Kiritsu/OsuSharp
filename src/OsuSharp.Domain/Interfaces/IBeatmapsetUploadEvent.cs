namespace OsuSharp.Interfaces;

public interface IBeatmapsetUploadEvent : IEvent
{
    IEventBeatmapsetModel Beatmapset { get; }
    IEventUserModel User { get; }
}