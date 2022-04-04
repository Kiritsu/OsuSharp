using System;

namespace OsuSharp.Legacy.Attributes;

/* 
 * Highly inspired from
 * DSharpPlus/Enums/Permission.cs: https://github.com/DSharpPlus/DSharpPlus/blob/master/DSharpPlus/Enums/Permission.cs
 * DSharpPlus/Utilities.cs: https://github.com/DSharpPlus/DSharpPlus/blob/master/DSharpPlus/Utilities.cs#L244-L256
 */
[AttributeUsage(AttributeTargets.Field)]
public sealed class ModeStringAttribute : Attribute
{
    public ModeStringAttribute(string str)
    {
        String = str;
    }

    public string String { get; }
}