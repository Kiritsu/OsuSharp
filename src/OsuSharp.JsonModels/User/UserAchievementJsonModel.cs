using System;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class UserAchievementJsonModel : JsonModel
{
    [JsonProperty("achieved_at")]
    public DateTimeOffset AchievedAt { get; set; }

    [JsonProperty("achievement_id")]
    public long AchievementId { get; set; }
}