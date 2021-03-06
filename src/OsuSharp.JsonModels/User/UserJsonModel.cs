﻿using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace OsuSharp.JsonModels
{
    public class UserJsonModel : UserCompactJsonModel
    {
        [JsonProperty("discord", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Discord { get; set; }

        [JsonProperty("has_supported")]
        public bool HasSupported { get; set; }

        [JsonProperty("interests", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Interests { get; set; }

        [JsonProperty("join_date")]
        public DateTimeOffset JoinDate { get; set; }

        [JsonProperty("kudosu")]
        public UserKudosuJsonModel Kudosu { get; set; }

        [JsonProperty("location", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Location { get; set; }

        [JsonProperty("max_blocks")]
        public long MaxBlocks { get; set; }

        [JsonProperty("max_friends")]
        public long MaxFriends { get; set; }

        [JsonProperty("occupation", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Occupation { get; set; }

        [JsonProperty("playmode")]
        public string GameMode { get; set; }

        [JsonProperty("playstyle")]
        public List<string> Playstyle { get; set; }

        [JsonProperty("post_count")]
        public long PostCount { get; set; }

        [JsonProperty("skype", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Skype { get; set; }

        [JsonProperty("title", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty("twitter", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Twitter { get; set; }

        [JsonProperty("website", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Website { get; set; }
    }
}