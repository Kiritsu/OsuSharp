using System.Collections.Generic;
using OsuSharp.Domain;

namespace OsuSharp.Interfaces;

public interface IUserGroup
{
    long Id { get; }
    string Identifier { get; }
    bool IsProbationary { get; }
    string Name { get; }
    string ShortName { get; }
    string Description { get; }
    string Colour { get; }
    IReadOnlyList<GameMode> PlayModes { get; }
}