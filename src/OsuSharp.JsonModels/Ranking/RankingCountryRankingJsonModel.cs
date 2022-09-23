using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class RankingCountryRankingJsonModel : JsonModel
{
      [JsonProperty("active_users")]
      public long ActiveUsers { get; set; }

      [JsonProperty("code")]
      public string Code { get; set; } = null!;

      [JsonProperty("country")]
      public UserCountryJsonModel Country { get; set; } = null!;

      [JsonProperty("performance")]
      public long Performance { get; set; }

      [JsonProperty("play_count")]
      public long PlayCount { get; set; }

      [JsonProperty("ranked_score")]
      public long RankedScore { get; set; }
}