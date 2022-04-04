using System;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class UserCoverJsonModel : JsonModel
{
    [JsonProperty("custom_url", DefaultValueHandling = DefaultValueHandling.Ignore)]
    public string CustomUrl { get; set; } = null!;

    [JsonProperty("url")]
    public Uri Url { get; set; } = null!;

    [JsonProperty("id")]
    public string Id { get; set; } = null!;
}