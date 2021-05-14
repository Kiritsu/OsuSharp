using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class UserRankHistoryJsonModel : JsonModel
    {
        [JsonProperty("mode")]
        public string GameMode { get; set; }

        [JsonProperty("data")]
        public IReadOnlyCollection<long> Ranks { get; set; }
    }
}