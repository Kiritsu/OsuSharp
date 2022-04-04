using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface IBeatmapsetApproveEvent : IEvent
{
    RankStatus Approval { get; }
    IEventBeatmapsetModel Beatmapset { get; }
    IEventUserModel User { get; }
}