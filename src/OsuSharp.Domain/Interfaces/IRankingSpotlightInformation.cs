using System;

namespace OsuSharp.Interfaces;

public interface IRankingSpotlightInformation
{
    DateTime EndTime { get; }
    int Id { get; }
    bool ModeSpecific { get; }
    int? ParticipantCount { get; }
    string Name { get; }
    DateTime StartDate { get; }
    string Type { get; }
}