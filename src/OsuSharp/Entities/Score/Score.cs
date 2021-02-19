using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using OsuSharp.Enums;

namespace OsuSharp.Entities
{
    public class Score
    {
        [JsonProperty("id")]
        public long Id { get; internal set; }
        
        [JsonProperty("best_id")]
        public long? BestId { get; internal set; }
        
        [JsonProperty("user_id")]
        public long UserId { get; internal set; }
        
        [JsonProperty("accuracy")]
        public double Accuracy { get; internal set; }
        
        [JsonProperty("mods")]
        public IReadOnlyList<string> Mods { get; internal set; }
        
        [JsonProperty("score")]
        public long TotalScore { get; internal set; }
        
        [JsonProperty("max_combo")]
        public int MaxCombo { get; internal set; }
        
        [JsonProperty("perfect")]
        public bool Perfect { get; internal set; }
        
        [JsonProperty("statistics")]
        public Statistics Statistics { get; internal set; }
        
        [JsonProperty("pp")]
        public int? PerformancePoints { get; internal set; }
        
        [JsonProperty("rank")]
        public string Rank { get; internal set; }
        
        [JsonProperty("created_at")]
        public DateTimeOffset CreatedAt { get; internal set; }
        
        [JsonProperty("mode")]
        public GameMode Mode { get; internal set; }
        
        [JsonProperty("replay")]
        public bool? HasReplay { get; internal set; }
        
        [JsonProperty("beatmap")]
        public Beatmap Beatmap { get; internal set; }
        
        [JsonProperty("beatmapset")]
        public Beatmapset Beatmapset { get; internal set; }
        
        [JsonProperty("rank_country")]
        public long? CountryRank { get; internal set; }
        
        [JsonProperty("rank_global")]
        public long? GlobalRank { get; internal set; }
        
        [JsonProperty("weight")]
        public double? Weight { get; internal set; }
        
        [JsonProperty("user")]
        public User User { get; internal set; }
        
        // todo: object
        [JsonProperty("match")]
        public object Match { get; internal set; }

        internal Score()
        {
            
        }
    }
}