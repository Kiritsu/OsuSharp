using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class FailTimesJsonModel : JsonModel
    {
        [JsonProperty("exit")]
        public IReadOnlyList<int> Exit { get; set; }

        [JsonProperty("fail")]
        public IReadOnlyList<int> Fail { get; set; }
    }
}