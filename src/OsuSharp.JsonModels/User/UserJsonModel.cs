using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public sealed class UserJsonModel : UserCompactJsonModel
    {
        [JsonProperty("cover_url")]
        public string CoverUrl { get; internal set; }
        
        [JsonProperty("discord", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Discord { get; internal set; }
        
        [JsonProperty("has_supported")]
        public bool HasSupported { get; internal set; }
        
        [JsonProperty("interests", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Interests { get; internal set; }
        
        [JsonProperty("join_date")]
        public DateTimeOffset JoinDate { get; internal set; }

        [JsonProperty("kudosu")]
        public UserKudosuJsonModel KudosuJsonModel { get; internal set; }
        
        [JsonProperty("location", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Location { get; internal set; }
        
        [JsonProperty("max_blocks")]
        public long MaxBlocks { get; internal set; }
        
        [JsonProperty("max_friends")]
        public long MaxFriends { get; internal set; }
        
        [JsonProperty("occupation", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Occupation { get; internal set; }
        
        [JsonProperty("playmode")]
        public GameMode GameMode { get; internal set; }
        
        [JsonProperty("playstyle")]
        public IReadOnlyCollection<string> Playstyle { get; internal set; }
        
        [JsonProperty("post_count")]
        public long PostCount { get; internal set; }
        
        [JsonProperty("skype", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Skype { get; internal set; }
        
        [JsonProperty("title", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Title { get; internal set; }
        
        [JsonProperty("twitter", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Twitter { get; internal set; }
        
        [JsonProperty("website", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string Website { get; internal set; }
        
        internal UserJsonModel()
        {
            
        }
    }
}