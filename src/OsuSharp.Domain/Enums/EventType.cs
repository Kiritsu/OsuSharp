namespace OsuSharp.Domain;

/// <summary>
/// Represents the different sent Event Types.
/// </summary>
public enum EventType
{
    Unknown,
    Achievement,
    BeatmapPlaycount,
    BeatmapsetApprove,
    BeatmapsetDelete,
    BeatmapsetRevive,
    BeatmapsetUpdate,
    BeatmapsetUpload,
    Rank,
    RankLost,
    UserSupportAgain,
    UserSupportFirst,
    UserSupportGift,
    UsernameChange
}