using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public sealed class FailTimesJsonModel
    {
        [JsonProperty("exit")]
        public IReadOnlyList<int> Exit { get; internal set; }

        [JsonProperty("fail")]
        public IReadOnlyList<int> Fail { get; internal set; }

        internal FailTimesJsonModel()
        {
        }
    }
}