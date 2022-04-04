using System;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class UserAccountHistoryJsonModel : JsonModel
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; } = null!;

    [JsonProperty("timestamp")]
    public DateTimeOffset TimeStamp { get; set; }

    [JsonProperty("length")]
    public int Length { get; set; }
}