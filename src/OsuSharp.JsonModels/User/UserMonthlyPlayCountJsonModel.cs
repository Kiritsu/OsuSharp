using System;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class UserMonthlyPlayCountJsonModel : JsonModel
{
    [JsonProperty("start_date")]
    public DateTimeOffset StartDate { get; set; }

    [JsonProperty("count")]
    public long Count { get; set; }
}