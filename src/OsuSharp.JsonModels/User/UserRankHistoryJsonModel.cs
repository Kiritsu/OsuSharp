using System.Collections.Generic;
using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public class UserRankHistoryJsonModel : JsonModel
    {
        [JsonProperty("mode")]
        public GameMode GameMode { get; set; }

        [JsonProperty("data")]
        public IReadOnlyCollection<long> Ranks { get; set; }
    }
}