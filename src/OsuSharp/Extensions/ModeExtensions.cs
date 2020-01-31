using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OsuSharp.Attributes;

namespace OsuSharp
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

        /// <summary>
        ///     Converts a <see cref="Mode"/> into a string separated with the modes separator from the <see cref="OsuSharpConfiguration"/>.
        /// </summary>
        /// <param name="mode">Mode to convert.</param>
        /// <param name="instance">Instance on which we use the mode separator.</param>
        /// <returns></returns>
        public static string ToModeString(this Mode mode, OsuClient instance)
        {
            if (mode == Mode.None)
            {
                return ModeStrings[Mode.None];
            }

            var modes = ModeStrings.Where(k => k.Key != Mode.None && (mode & k.Key) == k.Key).Select(k => k.Value);

            return string.Join(instance.OsuSharpConfiguration.ModeSeparator, modes);
        }

        /// <summary>
        ///     Converts a <see cref="string"/> into a Mode representation.
        /// </summary>
        /// <param name="value">Value to parse.</param>
        /// <param name="mode">Modes out.</param>
        /// <returns></returns>
        public static bool TryParse(string value, out Mode mode)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                mode = Mode.None;
                return false;
            }

            var modes = ModeStrings
                .Where(x => value.Contains(x.Value, StringComparison.OrdinalIgnoreCase))
                .Select(y => y.Key);

            mode = Mode.None;
            foreach (var m in modes)
            {
                if ((mode & m) != 0)
                {
                    continue;
                }

                mode |= m;
            }

            return true;
        }
    }
}
