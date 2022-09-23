using System;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public class RankingSpotlightInformation : IRankingSpotlightInformation
{
    public DateTime EndTime { get; set; }
    public int Id { get; set; }
    public bool ModeSpecific { get; set; }
    public int? ParticipantCount { get; internal set; } = null!;
    public string Name { get; internal set; } = null!;
    public DateTime StartDate { get; set; }
    public string Type { get; internal set; } = null!;

    internal RankingSpotlightInformation() { }
}