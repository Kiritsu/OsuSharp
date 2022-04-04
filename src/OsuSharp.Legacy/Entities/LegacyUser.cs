#pragma warning disable CS0649
using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using OsuSharp.Legacy.Enums;

namespace OsuSharp.Legacy.Entities;

public sealed class LegacyUser : LegacyEntityBase
{
    internal LegacyUser() { }

    /// <summary>
    ///     Id of the user.
    /// </summary>
    [JsonProperty("user_id")]
    public long UserId { get; internal set; }

    /// <summary>
    ///     Username of the user.
    /// </summary>
    [JsonProperty("username")]
    public string Username { get; internal set; }

    /// <summary>
    ///     Gets the date the user joined osu!.
    /// </summary>
    [JsonProperty("join_date")]
    public DateTimeOffset? JoinDate { get; internal set; }

    /// <summary>
    ///     Total amount of 50s for this user.
    /// </summary>
    [JsonProperty("count50")]
    public long? Count50 { get; internal set; }

    /// <summary>
    ///     Total amount of 100s for this user.
    /// </summary>
    [JsonProperty("count100")]
    public long? Count100 { get; internal set; }

    /// <summary>
    ///     Total amount of 300s for this user.
    /// </summary>
    [JsonProperty("count300")]
    public long? Count300 { get; internal set; }

    /// <summary>
    ///     Total amount of play count.
    /// </summary>
    [JsonProperty("playcount")]
    public long? PlayCount { get; internal set; }

    /// <summary>
    ///     Total amount of ranked score.
    /// </summary>
    [JsonProperty("ranked_score")]
    public long? RankedScore { get; internal set; }

    /// <summary>
    ///     Total amount of score.
    /// </summary>
    [JsonProperty("total_score")]
    public long? Score { get; internal set; }

    /// <summary>
    ///     Gets the global rank of the user.
    /// </summary>
    [JsonProperty("pp_rank")]
    public long? Rank { get; internal set; }

    /// <summary>
    ///     Gets the level of the user.
    /// </summary>
    [JsonProperty("level")]
    public double? Level { get; internal set; }

    /// <summary>
    ///     Gets the amount of performance points of the user.
    /// </summary>
    [JsonProperty("pp_raw")]
    public double? PerformancePoints { get; internal set; }

    /// <summary>
    ///     Gets the accuracy of the user.
    /// </summary>
    [JsonProperty("accuracy")]
    public double? Accuracy { get; internal set; }

    /// <summary>
    ///     Count of SS ranks.
    /// </summary>
    [JsonProperty("count_rank_ss")]
    public double? CountSS { get; internal set; }

    /// <summary>
    ///     Count of SS ranks. (with Flashlight or Hidden mods)
    /// </summary>
    [JsonProperty("count_rank_ssh")]
    public double? CountSSH { get; internal set; }

    /// <summary>
    ///     Count of S ranks.
    /// </summary>
    [JsonProperty("count_rank_s")]
    public double? CountS { get; internal set; }

    /// <summary>
    ///     Count of S ranks. (with Flashlight or Hidden mods)
    /// </summary>
    [JsonProperty("count_rank_sh")]
    public double? CountSH { get; internal set; }

    /// <summary>
    ///     Count of A ranks.
    /// </summary>
    [JsonProperty("count_rank_a")]
    public double? CountA { get; internal set; }

    /// <summary>
    ///     Gets the country of the user.
    /// </summary>
    [JsonIgnore]
    public RegionInfo Country => new(_country);

    [JsonProperty("country")]
    private readonly string _country;

    /// <summary>
    ///     Gets the time the user played osu!.
    /// </summary>
    [JsonIgnore]
    public TimeSpan TimePlayed => TimeSpan.FromSeconds(_totalSecondsPlayed ?? 0);

    [JsonProperty("total_seconds_played")]
    private readonly int? _totalSecondsPlayed;

    /// <summary>
    ///     Gets the country rank of the user.
    /// </summary>
    [JsonProperty("pp_country_rank")]
    public long? CountryRank { get; internal set; }

    /// <summary>
    ///     Gets the game mode this user's stats are for.
    /// </summary>
    [JsonIgnore]
    public GameMode GameMode { get; internal set; }

    /// <summary>
    ///     Gets the events of the user.
    /// </summary>
    [JsonProperty("events")]
    public IReadOnlyList<LegacyUserEvents> Events { get; internal set; }

    /// <summary>
    ///     Starts spectating the user.
    /// </summary>
    public Uri SpectateUri => new($"osu://spectate/{Username}");

    /// <summary>
    ///     User's osu profile uri.
    /// </summary>
    public Uri ProfileUri => new($"https://osu.ppy.sh/users/{UserId}");
}