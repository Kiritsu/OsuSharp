using System;

namespace OsuSharp.Domain
{
    public sealed class Beatmapset : BeatmapsetCompact
    {
        public BeatmapAvailability Availability { get; internal set; }

        public double Bpm { get; internal set; }

        public bool CanBeHyped { get; internal set; }

        public bool DiscussionEnabled { get; internal set; }

        public bool DiscussionLocked { get; internal set; }

        public BeatmapHype Hype { get; internal set; }

        public bool IsScoreable { get; internal set; }

        public DateTimeOffset LastUpdated { get; internal set; }

        public string LegacyThreadUrl { get; internal set; }

        public BeatmapNomination Nomination { get; internal set; }

        public RankStatus Ranked { get; internal set; }

        public DateTimeOffset? RankedDate { get; internal set; }

        public bool HasStoryboard { get; internal set; }

        public DateTimeOffset? SubmittedAt { get; internal set; }

        public string Tags { get; internal set; }
        
        internal Beatmapset()
        {
            
        }
    }
}