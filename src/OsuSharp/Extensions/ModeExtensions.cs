using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OsuSharp.Attributes;
using OsuSharp.Enums;

namespace OsuSharp.Extensions
{
    /* 
     * Highly inspired from
     * DSharpPlus/Enums/Permission.cs: https://github.com/DSharpPlus/DSharpPlus/blob/master/DSharpPlus/Enums/Permission.cs
     * DSharpPlus/Utilities.cs: https://github.com/DSharpPlus/DSharpPlus/blob/master/DSharpPlus/Utilities.cs#L244-L256
     */
    public static class ModeExtensions
    {
        private static Dictionary<Mode, string> ModeStrings { get; }

        static ModeExtensions()
        {
            ModeStrings = new Dictionary<Mode, string>();

            var t = typeof(Mode);
            var ti = t.GetTypeInfo();
            var mods = Enum.GetValues(t).Cast<Mode>();

            foreach (var mod in mods)
            {
                ModeStrings[mod] = ti.DeclaredMembers
                    .FirstOrDefault(xm => xm.Name == mod.ToString())
                    .GetCustomAttribute<ModeStringAttribute>().String;
            }
        }

        public static string ToModeStrings(this Mode mode, OsuApi instance)
        {
            if (mode == Mode.None)
            {
                return ModeStrings[Mode.None];
            }

            var modes = ModeStrings.Where(k => k.Key != Mode.None && (mode & k.Key) == k.Key).Select(k => k.Value);

            return string.Join(instance.OsuSharpConfiguration.ModsSeparator, modes);
        }
    }
}
