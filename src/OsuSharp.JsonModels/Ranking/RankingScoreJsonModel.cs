using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class RankingScoreJsonModel : JsonModel
{
      [JsonProperty("cursor")]
      public RankingCursorJsonModel Cursor { get; set; } = null!;

      [JsonProperty("ranking")]
      public List<UserStatisticsJsonModel> Ranking { get; set; } = null!;

      [JsonProperty("total")]
      public int Total { get; set; }
}