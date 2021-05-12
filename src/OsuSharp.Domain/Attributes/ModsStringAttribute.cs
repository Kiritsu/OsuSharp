using System;

namespace OsuSharp.Domain
{
    /* 
     * Highly inspired from
     * DSharpPlus/Enums/Permission.cs: https://github.com/DSharpPlus/DSharpPlus/blob/master/DSharpPlus/Enums/Permission.cs
     * DSharpPlus/Utilities.cs: https://github.com/DSharpPlus/DSharpPlus/blob/master/DSharpPlus/Utilities.cs#L244-L256
     */
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ModsStringAttribute : Attribute
    {
        public ModsStringAttribute(string str)
        {
            String = str;
        }

        public string String { get; }
    }
}