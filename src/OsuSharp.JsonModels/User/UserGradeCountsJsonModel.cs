﻿using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class UserGradeCountsJsonModel : JsonModel
    {
        [JsonProperty("ss")]
        public long SS { get; set; }

        [JsonProperty("ssh")]
        public long SSH { get; set; }

        [JsonProperty("s")]
        public long S { get; set; }

        [JsonProperty("sh")]
        public long SH { get; set; }

        [JsonProperty("a")]
        public long A { get; set; }
    }
}