using System;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class KudosuHistoryJsonModel : JsonModel
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("action")]
    public string Action { get; set; } = null!;

    [JsonProperty("amount")]
    public long Amount { get; set; }

    //todo: make enum
    [JsonProperty("model")]
    public string Model { get; set; } = null!;

    [JsonProperty("created_at")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonProperty("giver")]
    public KudosuGiverJsonModel Giver { get; set; } = null!;

    [JsonProperty("post")]
    public KudosuPostJsonModel Post { get; set; } = null!;
}