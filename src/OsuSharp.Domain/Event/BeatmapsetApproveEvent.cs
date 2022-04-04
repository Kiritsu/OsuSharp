using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class BeatmapsetApproveEvent : Event, IBeatmapsetApproveEvent
{
    public RankStatus Approval { get; internal set; }

    public IEventBeatmapsetModel Beatmapset { get; internal set; } = null!;

    public IEventUserModel User { get; internal set; } = null!;

    internal BeatmapsetApproveEvent()
    {
            
    }
}