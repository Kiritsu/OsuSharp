using System;
using Newtonsoft.Json;
using OsuSharp.Domain;

namespace OsuSharp.JsonModels
{
    public class BeatmapsetJsonModel : BeatmapsetCompactJsonModel
    {
        [JsonProperty("availability")]
        public BeatmapAvailabilityJsonModel AvailabilityJsonModel { get; set; }

        [JsonProperty("bpm")]
        public double Bpm { get; set; }

        [JsonProperty("can_be_hyped")]
        public bool CanBeHyped { get; set; }

        [JsonProperty("discussion_enabled")]
        public bool DiscussionEnabled { get; set; }

        [JsonProperty("discussion_locked")]
        public bool DiscussionLocked { get; set; }

        [JsonProperty("hype")]
        public BeatmapHypeJsonModel HypeJsonModel { get; set; }

        [JsonProperty("is_scoreable")]
        public bool IsScoreable { get; set; }

        [JsonProperty("last_updated")]
        public DateTimeOffset LastUpdated { get; set; }

        [JsonProperty("legacy_thread_url")]
        public string LegacyThreadUrl { get; set; }

        [JsonProperty("nomination")]
        public BeatmapNominationJsonModel NominationJsonModel { get; set; }

        [JsonProperty("ranked")]
        public RankStatus Ranked { get; set; }

        [JsonProperty("ranked_date")]
        public DateTimeOffset? RankedDate { get; set; }

        [JsonProperty("storyboard")]
        public bool HasStoryboard { get; set; }

        [JsonProperty("submitted_date")]
        public DateTimeOffset? SubmittedAt { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }
    }
}