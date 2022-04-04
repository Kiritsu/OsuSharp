using System;
using System.Collections.Generic;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

/// <summary>
/// Defines a beatmapset object.
/// </summary>
public interface IBeatmapset : IBeatmapsetCompact
{
    IBeatmapAvailability Availability { get; }
    double Bpm { get; }
    bool CanBeHyped { get; }
    bool DiscussionEnabled { get; }
    bool DiscussionLocked { get; }
    IBeatmapHype Hype { get; }
    bool IsScoreable { get; }
    DateTimeOffset LastUpdated { get; }
    string LegacyThreadUrl { get; }
    IBeatmapNomination Nomination { get; }
    RankStatus Ranked { get; }
    DateTimeOffset? RankedDate { get; }
    bool HasStoryboard { get; }
    DateTimeOffset? SubmittedAt { get; }
    string Tags { get; }
    IReadOnlyList<int> Ratings { get; }
    long? TrackId { get; }
}