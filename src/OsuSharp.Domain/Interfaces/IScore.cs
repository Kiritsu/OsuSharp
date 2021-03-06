﻿using System;
using System.Collections.Generic;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces
{
    public interface IScore
    {
        long Id { get; }
        long? BestId { get; }
        long UserId { get; }
        double Accuracy { get; }
        IReadOnlyList<string> Mods { get; }
        long TotalScore { get; }
        int MaxCombo { get; }
        bool Perfect { get; }
        IStatistics Statistics { get; }
        double? PerformancePoints { get; }
        string Rank { get; }
        DateTimeOffset CreatedAt { get; }
        GameMode Mode { get; }
        bool? HasReplay { get; }
        IBeatmap Beatmap { get; }
        IBeatmapset Beatmapset { get; }
        long? CountryRank { get; }
        long? GlobalRank { get; }
        IWeight Weight { get; }
        IUser User { get; }
        object Match { get; }
    }
}