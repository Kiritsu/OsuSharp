using Newtonsoft.Json;
using System.Collections.Generic;

namespace OsuSharp.JsonModels
{
    public class BeatmapsetSearchEnumerationJsonModel : JsonModel
    {
        [JsonProperty("beatmapsets")]
        public List<BeatmapsetJsonModel> Beatmapsets { get; set; }

        [JsonProperty("cursor")]
        public CursorJsonModel Cursor { get; set; }

        [JsonProperty("search")]
        public SearchJsonModel Search { get; set; }

        [JsonProperty("recommended_difficulty")]
        public double? RecommendedDifficulty { get; set; }

        [JsonProperty("error")]
        public object Error { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }
}
