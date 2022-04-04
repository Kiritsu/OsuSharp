using System;
using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class Beatmapset : BeatmapsetCompact, IBeatmapset
{
    public IBeatmapAvailability Availability { get; internal set; } = null!;

    public double Bpm { get; internal set; }

    public bool CanBeHyped { get; internal set; }

    public bool DiscussionEnabled { get; internal set; }

    public bool DiscussionLocked { get; internal set; }

    public IBeatmapHype Hype { get; internal set; } = null!;

    public bool IsScoreable { get; internal set; }

    public DateTimeOffset LastUpdated { get; internal set; }

    public string LegacyThreadUrl { get; internal set; } = null!;

    public IBeatmapNomination Nomination { get; internal set; } = null!;

    public RankStatus Ranked { get; internal set; }

    public DateTimeOffset? RankedDate { get; internal set; }

    public bool HasStoryboard { get; internal set; }

    public DateTimeOffset? SubmittedAt { get; internal set; }

    public string Tags { get; internal set; } = null!;

    public IReadOnlyList<int> Ratings { get; internal set; } = null!;
    
    public long? TrackId { get; set; }

    internal Beatmapset()
    {
            
    }
}