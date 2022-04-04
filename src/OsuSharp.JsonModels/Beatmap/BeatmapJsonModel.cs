using System;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class BeatmapJsonModel : JsonModel
{
    [JsonProperty("difficulty_rating")]
    public double DifficultyRating { get; set; }

    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("mode")]
    public string Mode { get; set; } = null!;

    [JsonProperty("status")]
    public string Status { get; set; } = null!;

    [JsonProperty("total_length")]
    public long TotalLengthSeconds { get; set; }

    [JsonProperty("version")]
    public string Version { get; set; } = null!;

    [JsonProperty("beatmapset")]
    public BeatmapsetJsonModel Beatmapset { get; set; } = null!;

    [JsonProperty("checksum")]
    public string Checksum { get; set; } = null!;

    [JsonProperty("failtimes")]
    public FailTimesJsonModel FailTimes { get; set; } = null!;

    [JsonProperty("max_combo")]
    public int? MaxCombo { get; set; }

    [JsonProperty("user_id")]
    public long UserId { get; set; }

    [JsonProperty("accuracy")]
    public double Accuracy { get; set; }

    [JsonProperty("ar")]
    public double ApproachRate { get; set; }

    [JsonProperty("beatmapset_id")]
    public long BeatmapsetId { get; set; }

    [JsonProperty("bpm")]
    public double Bpm { get; set; }

    [JsonProperty("convert")]
    public bool Converted { get; set; }

    [JsonProperty("count_circles")]
    public int CircleCount { get; set; }

    [JsonProperty("count_sliders")]
    public int SliderCount { get; set; }

    [JsonProperty("count_spinners")]
    public int SpinnerCount { get; set; }

    [JsonProperty("cs")]
    public double CircleSize { get; set; }

    [JsonProperty("deleted_at")]
    public DateTimeOffset? DeletedAt { get; set; }

    [JsonProperty("drain")]
    public double Drain { get; set; }

    [JsonProperty("hit_length")]
    public int HitLengthSeconds { get; set; }

    [JsonProperty("is_scoreable")]
    public bool IsScoreable { get; set; }

    [JsonProperty("last_updated")]
    public DateTimeOffset LastUpdated { get; set; }

    [JsonProperty("passcount")]
    public int PassCount { get; set; }

    [JsonProperty("playcount")]
    public int PlayCount { get; set; }

    [JsonProperty("ranked")]
    public string Ranked { get; set; } = null!;

    [JsonProperty("url")]
    public string Url { get; set; } = null!;

    [JsonProperty("mode_int")]
    public int GameMode { get; set; }
}