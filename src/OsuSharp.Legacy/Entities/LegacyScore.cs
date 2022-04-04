#pragma warning disable CS0649
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OsuSharp.Legacy.Enums;

namespace OsuSharp.Legacy.Entities;

public sealed class LegacyScore : LegacyEntityBase
{
    internal LegacyScore() { }

    /// <summary>
    ///     Id of the beatmap.
    /// </summary>
    [JsonProperty("beatmap_id")]
    public long BeatmapId { get; set; }

    /// <summary>
    ///     Total score of the play.
    /// </summary>
    [JsonProperty("score")]
    public long TotalScore { get; internal set; }

    /// <summary>
    ///     Score id.
    /// </summary>
    [JsonProperty("score_id")]
    public long? ScoreId { get; internal set; }

    /// <summary>
    ///     Username of the player.
    /// </summary>
    [JsonProperty("username")]
    public string Username { get; internal set; }

    /// <summary>
    ///     Count of 300.
    /// </summary>
    [JsonProperty("count300")]
    public int Count300 { get; internal set; }

    /// <summary>
    ///     Count of 100.
    /// </summary>
    [JsonProperty("count100")]
    public int Count100 { get; internal set; }

    /// <summary>
    ///     Count of 50.
    /// </summary>
    [JsonProperty("count50")]
    public int Count50 { get; internal set; }

    /// <summary>
    ///     Count of misses.
    /// </summary>
    [JsonProperty("countmiss")]
    public int Miss { get; internal set; }

    /// <summary>
    ///     Accuracy of the play.
    /// </summary>
    [JsonIgnore]
    public double Accuracy
        => (Count50 * 50 + Count100 * 100 + Count300 * 300)
            / (300.0 * (Count50 + Count100 + Count300 + Miss)) * 100;

    /// <summary>
    ///     Max combo of the play.
    /// </summary>
    [JsonProperty("maxcombo")]
    public int? MaxCombo { get; internal set; }

    /// <summary>
    ///     Count of katus.
    /// </summary>
    [JsonProperty("countkatu")]
    public int Katu { get; internal set; }

    /// <summary>
    ///     Count of gekies.
    /// </summary>
    [JsonProperty("countgeki")]
    public int Geki { get; internal set; }

    /// <summary>
    ///     Indicates whether this score is full combo perfect.
    /// </summary>
    public bool Perfect => _perfect == 1;

    [JsonProperty("perfect")]
    private readonly int _perfect;

    /// <summary>
    ///     Mods enabled for this score.
    /// </summary>
    [JsonProperty("enabled_mods")]
    public Mode Mods { get; internal set; }

    /// <summary>
    ///     Id of the player.
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; internal set; }

    /// <summary>
    ///     Date the score was submitted.
    /// </summary>
    [JsonProperty("date")]
    public DateTimeOffset? Date { get; internal set; }

    /// <summary>
    ///     Indicates the rank of the map. (SS, SSH, S, SH, A, B, C, D)
    /// </summary>
    [JsonProperty("rank")]
    public string Rank { get; internal set; }

    /// <summary>
    ///     Performance points given by the map.
    /// </summary>
    [JsonProperty("pp")]
    public float? PerformancePoints { get; internal set; }

    /// <summary>
    ///     Indicates whether the replay is available.
    /// </summary>
    [JsonIgnore]
    public bool ReplayAvailable => _replayAvailable == 1;

    [JsonProperty("replay_available")]
    private readonly int _replayAvailable;

    /// <summary>
    ///     Game mode played for that score.
    /// </summary>
    [JsonIgnore]
    public GameMode GameMode { get; internal set; }

    /// <summary>
    ///     Gets the beatmap entity associated with the score.
    /// </summary>
    /// <returns></returns>
    public Task<LegacyBeatmap> GetBeatmapAsync()
    {
        return Client.GetBeatmapByIdAsync(BeatmapId, GameMode);
    }

    /// <summary>
    ///     Gets the entire beatmapset associated with the score.
    /// </summary>
    /// <returns></returns>
    public async Task<IReadOnlyList<LegacyBeatmap>> GetBeatmapsetAsync()
    {
        var beatmap = await GetBeatmapAsync().ConfigureAwait(false);
        return await Client.GetBeatmapsetAsync(beatmap.BeatmapsetId, GameMode);
    }

    /// <summary>
    ///     Gets the user that made this score.
    /// </summary>
    /// <returns></returns>
    public Task<LegacyUser> GetUserAsync()
    {
        return Client.GetUserByUserIdAsync(UserId, GameMode);
    }

    /// <summary>
    ///     Gets the replay of this beatmap if it's available.
    /// </summary>
    /// <returns></returns>
    public Task<LegacyReplay> GetReplayAsync()
    {
        if (!ReplayAvailable)
        {
            throw new InvalidOperationException("This replay is unavailable.");
        }

        return Client.GetReplayByUserIdAsync(BeatmapId, UserId, GameMode);
    }
}