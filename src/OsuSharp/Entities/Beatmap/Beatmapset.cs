using System;
using Newtonsoft.Json;
using OsuSharp.Enums;

namespace OsuSharp.Entities
{
    public sealed class Beatmapset : BeatmapsetCompact
    {
        [JsonProperty("availability.download_disabled")]
        public bool DownloadDisabled { get; internal set; }
        
        [JsonProperty("availability.more_information")]
        public Optional<string> MoreInformation { get; internal set; }
        
        [JsonProperty("bpm")]
        public double Bpm { get; internal set; }
        
        [JsonProperty("can_be_hyped")]
        public bool CanBeHyped { get; internal set; }

        [JsonProperty("discussion_enabled")]
        public bool DiscussionEnabled { get; internal set; }
        
        [JsonProperty("discussion_locked")]
        public bool DiscussionLocked { get; internal set; }
        
        [JsonProperty("hype.current")]
        public int? CurrentHype { get; internal set; }
        
        [JsonProperty("hype.required")]
        public int? RequiredHype { get; internal set; }
        
        [JsonProperty("is_scoreable")]
        public bool IsScoreable { get; internal set; }
        
        [JsonProperty("last_updated")]
        public DateTimeOffset LastUpdated { get; internal set; }
        
        [JsonProperty("legacy_thread_url")]
        public string LegacyThreadUrl { get; internal set; }
        
        [JsonProperty("nomination.current")]
        public int CurrentNomination { get; internal set; }
        
        [JsonProperty("nomination.required")]
        public int RequiredNomination { get; internal set; }
        
        [JsonProperty("ranked")]
        public RankStatus Ranked { get; internal set; }
        
        [JsonProperty("ranked_date")]
        public Optional<DateTimeOffset> RankedDate { get; internal set; }

        [JsonProperty("storyboard")]
        public bool HasStoryboard { get; internal set; }
        
        [JsonProperty("submitted_date")]
        public Optional<DateTimeOffset> SubmittedAt { get; internal set; }
        
        [JsonProperty("tags")]
        public string Tags { get; internal set; }
        
        internal Beatmapset()
        {
            
        }
    }
}