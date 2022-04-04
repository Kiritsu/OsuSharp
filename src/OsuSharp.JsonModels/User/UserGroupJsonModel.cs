using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class UserGroupJsonModel : JsonModel
{
    [JsonProperty("id")]
    public long Id { get; set; }

    [JsonProperty("identifier")]
    public string Identifier { get; set; } = null!;

    [JsonProperty("is_probationary")]
    public bool IsProbationary { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; } = null!;

    [JsonProperty("short_name")]
    public string ShortName { get; set; } = null!;

    [JsonProperty("description")]
    public string Description { get; set; } = null!;

    [JsonProperty("colour")]
    public string Colour { get; set; } = null!;

    [JsonProperty("play_modes")]
    public List<string> PlayModes { get; set; } = null!;
}