using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class RankingSpotlightJsonModel : JsonModel
{
      [JsonProperty("beatmapsets")]
      public List<BeatmapsetJsonModel> Beatmapsets { get; set; } = null!;

      [JsonProperty("cursor")]
      public RankingCursorJsonModel Cursor { get; set; } = null!;

      [JsonProperty("ranking")]
      public List<UserStatisticsJsonModel> Ranking { get; set; } = null!;

      [JsonProperty("spotlight")]
      public RankingSpotlightInformationJsonModel Spotlight { get; set; } = null!;

      [JsonProperty("total")]
      public int Total { get; set; }
}