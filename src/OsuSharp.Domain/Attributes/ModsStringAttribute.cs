using System;

namespace OsuSharp.Domain;
/* 
 * Highly inspired from
 * DSharpPlus/Enums/Permission.cs: https://github.com/DSharpPlus/DSharpPlus/blob/master/DSharpPlus/Enums/Permission.cs
 * DSharpPlus/Utilities.cs: https://github.com/DSharpPlus/DSharpPlus/blob/master/DSharpPlus/Utilities.cs#L244-L256
 */

/// <summary>
/// Attributes that bind an enum member with a specific string name.
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Enum)]
public sealed class ModsStringAttribute : Attribute
{
    /// <summary>
    /// Creates a new instance of the attribute.
    /// </summary>
    /// <param name="str">Name to bind to the enum member.</param>
    public ModsStringAttribute(string str)
    {
        String = str;
    }

    /// <summary>
    /// Gets the name that is bound to the enum member.
    /// </summary>
    public string String { get; }
}