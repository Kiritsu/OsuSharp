using System;
using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public class KudosuHistoryJsonModel : JsonModel
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("action")]
        public KudosuAction Action { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        //todo: make enum
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; set; }

        [JsonProperty("giver")]
        public KudosuGiverJsonModel GiverJsonModel { get; set; }

        [JsonProperty("post")]
        public KudosuPostJsonModel PostJsonModel { get; set; }
    }
}