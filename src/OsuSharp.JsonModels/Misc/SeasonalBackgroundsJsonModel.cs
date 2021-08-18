﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class SeasonalBackgroundsJsonModel : JsonModel
    {
        [JsonProperty("ends_at")]
        public DateTimeOffset EndsAt { get; set; }

        [JsonProperty("backgrounds")]
        public List<SeasonalBackgroundJsonModel> Backgrounds { get; set; }
    }

    public class SeasonalBackgroundJsonModel : JsonModel
    {
        [JsonProperty("url")]
        public string Url { get; set;  }

        [JsonProperty("user")]
        public UserCompactJsonModel User { get; set; }
    }
}
