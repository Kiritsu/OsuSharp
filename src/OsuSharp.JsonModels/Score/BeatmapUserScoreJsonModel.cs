﻿using Newtonsoft.Json;

namespace OsuSharp.JsonModels.Score
{
    public class BeatmapUserScoreJsonModel : JsonModel
    {
        [JsonProperty("position")]
        public int Position { get; internal set; }

        [JsonProperty("score")]
        public ScoreJsonModel Score { get; internal set; }
    }
}
