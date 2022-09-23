using Newtonsoft.Json;

namespace OsuSharp.JsonModels;

public class RankingCursorJsonModel : JsonModel
{
      [JsonProperty("page")]
      public int? Page { get; set; }
}