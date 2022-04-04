using System.Collections.Generic;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class UserGroup : IUserGroup
{
    public long Id { get; internal set; }

    public string Identifier { get; internal set; } = null!;

    public bool IsProbationary { get; internal set; }

    public string Name { get; internal set; } = null!;

    public string ShortName { get; internal set; } = null!;

    public string Description { get; internal set; } = null!;

    public string Colour { get; internal set; } = null!;

    public IReadOnlyList<GameMode> PlayModes { get; internal set; } = null!;

    internal UserGroup()
    {
            
    }
}