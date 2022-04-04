using System;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public abstract class EventJsonModel : JsonModel
{
    [JsonProperty("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; } = null!;
}