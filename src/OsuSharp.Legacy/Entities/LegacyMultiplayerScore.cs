#pragma warning disable CS0649
using System.Threading.Tasks;
using Newtonsoft.Json;
using OsuSharp.Legacy.Enums;

namespace OsuSharp.Legacy.Entities;

public sealed class LegacyMultiplayerScore : LegacyEntityBase
{
    internal LegacyMultiplayerScore() { }

    /// <summary>
    ///     Slots on which the user is for this score.
    /// </summary>
    [JsonProperty("slot")]
    public int Slot { get; internal set; }

    /// <summary>
    ///     Team id on which the user is for this score.
    /// </summary>
    [JsonProperty("team")]
    public int Team { get; internal set; }

    /// <summary>
    ///     User id that played that beatmap.
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; internal set; }

    /// <summary>
    ///     Score that this player has done on the map.
    /// </summary>
    [JsonProperty("score")]
    public long Score { get; internal set; }

    /// <summary>
    ///     Max combo of the player for this score.
    /// </summary>
    [JsonProperty("maxcombo")]
    public long MaxCombo { get; internal set; }

    /// <summary>
    ///     Rank of the user for this map in the multiplayer room. Not used.
    /// </summary>
    [JsonProperty("rank")]
    public long Rank { get; internal set; }

    /// <summary>
    ///     Amount of 50s for this score.
    /// </summary>
    [JsonProperty("count50")]
    public long Count50 { get; internal set; }

    /// <summary>
    ///     Amount of 100s for this score.
    /// </summary>
    [JsonProperty("count100")]
    public long Count100 { get; internal set; }

    /// <summary>
    ///     Amount of 300s for this score.
    /// </summary>
    [JsonProperty("count300")]
    public long Count300 { get; internal set; }

    /// <summary>
    ///     Amount of miss for this score.
    /// </summary>
    [JsonProperty("countmiss")]
    public long CountMiss { get; internal set; }

    /// <summary>
    ///     Amount of geki for this score.
    /// </summary>
    [JsonProperty("countgeki")]
    public long CountGeki { get; internal set; }

    /// <summary>
    ///     Amount of katu for this score.
    /// </summary>
    [JsonProperty("countkatu")]
    public long CountKatu { get; internal set; }

    /// <summary>
    ///     Indicates whether this score is a full combo.
    /// </summary>
    [JsonIgnore]
    public bool Perfect => _perfect == 1;

    [JsonProperty("perfect")]
    private readonly int _perfect;

    /// <summary>
    ///     Indicates whether the player has passed the map..
    /// </summary>
    [JsonIgnore]
    public bool Pass => _pass == 1;

    [JsonProperty("pass")]
    private readonly int _pass;

    /// <summary>
    ///     Gets the mods by user for that song.
    /// </summary>
    [JsonProperty("mods")]
    public Mode? Modes { get; internal set; }

    /// <summary>
    ///     Gets the user that made this score.
    /// </summary>
    /// <returns></returns>
    public Task<LegacyUser> GetUserAsync(GameMode gameMode)
    {
        return Client.GetUserByUserIdAsync(UserId, gameMode);
    }
}