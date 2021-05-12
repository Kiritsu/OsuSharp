using System;
using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public sealed class KudosuHistoryJsonModel : JsonModel
    {
        [JsonProperty("id")]
        public long Id { get; internal set; }

        [JsonProperty("action")]
        public KudosuAction Action { get; internal set; }

        [JsonProperty("amount")]
        public long Amount { get; internal set; }

        //todo: make enum
        [JsonProperty("model")]
        public string Model { get; internal set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; internal set; }

        [JsonProperty("giver")]
        public KudosuGiverJsonModel GiverJsonModel { get; internal set; }

        [JsonProperty("post")]
        public KudosuPostJsonModel PostJsonModel { get; internal set; }

        internal KudosuHistoryJsonModel()
        {

        }
    }
}
