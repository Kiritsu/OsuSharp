using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using OsuSharp.Domain;
using OsuSharp.Interfaces;

namespace OsuSharp.Extensions;

/* 
 * Highly inspired from
 * DSharpPlus/Enums/Permission.cs: https://github.com/DSharpPlus/DSharpPlus/blob/1018351390a3a360f0a48430ab65771cfddd81c7/DSharpPlus/Enums/Permission.cs
 * DSharpPlus/Utilities.cs: https://github.com/DSharpPlus/DSharpPlus/blob/1018351390a3a360f0a48430ab65771cfddd81c7/DSharpPlus/Utilities.cs#L54-L66
 */
public static class ModsExtensions
{
    private static Dictionary<Mods, string> ModeStrings { get; }

    static ModsExtensions()
    {
        ModeStrings = new Dictionary<Mods, string>();

        var t = typeof(Mods);
        var ti = t.GetTypeInfo();
        var mods = Enum.GetValues(t).Cast<Mods>();

        foreach (var mod in mods)
        {
            ModeStrings[mod] = ti.DeclaredMembers
                .FirstOrDefault(xm => xm.Name == mod.ToString())!
                .GetCustomAttribute<ModsStringAttribute>()!.String;
        }
    }

    /// <summary>
    /// Converts a <see cref="Mods"/> into a string separated with the mods separator from the <see cref="OsuClientConfiguration"/>.
    /// </summary>
    /// <param name="mode">Mod to convert.</param>
    /// <param name="separator">Separator to use between each mod.</param>
    /// <returns></returns>
    public static string ToModString(this Mods mode, string separator)
    {
        if (mode == Mods.None)
        {
            return ModeStrings[Mods.None];
        }

        var modes = ModeStrings
            .Where(k => k.Key != Mods.None && (mode & k.Key) == k.Key)
            .Select(k => k.Value);

        return string.Join(separator, modes);
    }

    /// <summary>
    /// Converts a <see cref="Mods"/> into a string separated with the modes separator from the <see cref="OsuClientConfiguration"/>.
    /// </summary>
    /// <param name="mode">Mode to convert.</param>
    /// <param name="instance">Instance on which we use the mode separator.</param>
    /// <returns></returns>
    public static string ToModString(this Mods mode, IOsuClient instance)
    {
        return mode.ToModString(instance.Configuration.ModFormatSeparator);
    }

    /// <summary>
    /// Converts a <see cref="string"/> into a Mode representation.
    /// </summary>
    /// <param name="value">Value to parse.</param>
    /// <param name="mode">Modes out.</param>
    /// <returns></returns>
    public static bool TryParse(string value, out Mods mode)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            mode = Mods.None;
            return false;
        }

        var modes = ModeStrings
            .Where(x => value.ToLower().Contains(x.Value.ToLower()))
            .Select(y => y.Key);

        mode = Mods.None;
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