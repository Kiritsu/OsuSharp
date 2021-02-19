using System;
using Newtonsoft.Json;
using OsuSharp.Enums;

namespace OsuSharp.Entities
{
    public sealed class Beatmap : BeatmapCompact
    {
        [JsonProperty("accuracy")]
        public double Accuracy { get; internal set; }
        
        [JsonProperty("ar")]
        public double ApproachRate { get; internal set; }
        
        [JsonProperty("beatmapset_id")]
        public long BeatmapsetId { get; internal set; }
        
        [JsonProperty("bpm")]
        public double Bpm { get; internal set; }
        
        [JsonProperty("convert")]
        public bool Converted { get; internal set; }
        
        [JsonProperty("count_circles")]
        public int CircleCount { get; internal set; }
        
        [JsonProperty("count_sliders")]
        public int SliderCount { get; internal set; }
        
        [JsonProperty("count_spinners")]
        public int SpinnerCount { get; internal set; }
        
        [JsonProperty("cs")]
        public double CircleSize { get; internal set; }
        
        [JsonProperty("deleted_at")]
        public DateTimeOffset? DeletedAt { get; internal set; }
        
        [JsonProperty("drain")]
        public double Drain { get; internal set; }
        
        [JsonProperty("hit_length")]
        public int HitLength { get; internal set; }
        
        [JsonProperty("is_scoreable")]
        public bool IsScoreable { get; internal set; }
        
        [JsonProperty("last_updated")]
        public DateTimeOffset LastUpdated { get; internal set; }
        
        [JsonProperty("passcount")]
        public int PassCount { get; internal set; }
        
        [JsonProperty("playcount")]
        public int PlayCount { get; internal set; }
        
        [JsonProperty("ranked")]
        public RankStatus Ranked { get; internal set; }
        
        [JsonProperty("url")]
        public string Url { get; internal set; }

        internal Beatmap()
        {
            
        }
    }
}