using System;
using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public sealed class BeatmapsetJsonModel : BeatmapsetCompactJsonModel
    {
        [JsonProperty("availability")]
        public BeatmapAvailabilityJsonModel AvailabilityJsonModel { get; internal set; }
        
        [JsonProperty("bpm")]
        public double Bpm { get; internal set; }
        
        [JsonProperty("can_be_hyped")]
        public bool CanBeHyped { get; internal set; }

        [JsonProperty("discussion_enabled")]
        public bool DiscussionEnabled { get; internal set; }
        
        [JsonProperty("discussion_locked")]
        public bool DiscussionLocked { get; internal set; }
        
        [JsonProperty("hype")]
        public BeatmapHypeJsonModel HypeJsonModel { get; internal set; }
        
        [JsonProperty("is_scoreable")]
        public bool IsScoreable { get; internal set; }
        
        [JsonProperty("last_updated")]
        public DateTimeOffset LastUpdated { get; internal set; }
        
        [JsonProperty("legacy_thread_url")]
        public string LegacyThreadUrl { get; internal set; }

        [JsonProperty("nomination")]
        public BeatmapNominationJsonModel NominationJsonModel { get; internal set; }
        
        [JsonProperty("ranked")]
        public RankStatus Ranked { get; internal set; }
        
        [JsonProperty("ranked_date")]
        public DateTimeOffset? RankedDate { get; internal set; }

        [JsonProperty("storyboard")]
        public bool HasStoryboard { get; internal set; }
        
        [JsonProperty("submitted_date")]
        public DateTimeOffset? SubmittedAt { get; internal set; }
        
        [JsonProperty("tags")]
        public string Tags { get; internal set; }
        
        internal BeatmapsetJsonModel()
        {
            
        }
    }
}