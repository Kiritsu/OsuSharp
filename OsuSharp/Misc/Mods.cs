using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace OsuSharp.Misc
{
    [Flags]
    public enum Mods
    {
        [ModsString("No mode")] None = 0,

        [ModsString("NF")] NoFail = 1,

        [ModsString("EZ")] Easy = 2,

        [ModsString("NV")] NoVideo = 4,

        [ModsString("HD")] Hidden = 8,

        [ModsString("HR")] HardRock = 16,

        [ModsString("SD")] SuddenDeath = 32,

        [ModsString("DT")] DoubleTime = 64,

        [ModsString("RX")] Relax = 128,

        [ModsString("HT")] HalfTime = 256,

        [ModsString("NC")] Nightcore = 512,

        [ModsString("FL")] Flashlight = 1024,

        [ModsString("Auto")] Autoplay = 2048,

        [ModsString("SO")] SpunOut = 4096,

        [ModsString("AP")] Relax2 = 8192,

        [ModsString("PF")] Perfect = 16384,

        [ModsString("4K")] Key4 = 32768,

        [ModsString("5K")] Key5 = 65536,

        [ModsString("6K")] Key6 = 131072,

        [ModsString("7K")] Key7 = 262144,

        [ModsString("8K")] Key8 = 524288,

        [ModsString("KeyMod")] KeyMod = Key4 | Key5 | Key6 | Key7 | Key8,

        [ModsString("FadeIn")] FadeIn = 1048576,

        [ModsString("Random")] Random = 2097152,

        [ModsString("LastMod")] LastMod = 4194304,

        [ModsString("FreeModAllowed")] FreeModAllowed = NoFail | Easy | Hidden | HardRock | SuddenDeath | Flashlight | FadeIn | Relax | Relax2 | SpunOut | KeyMod,

        [ModsString("9K")] Key9 = 16777216,

        [ModsString("10K")] Key10 = 33554432,

        [ModsString("1K")] Key1 = 67108864,

        [ModsString("3K")] Key3 = 134217728,

        [ModsString("2K")] Key2 = 268435456
    }

    [AttributeUsage(AttributeTargets.Field)]
    public sealed class ModsStringAttribute : Attribute
    {
        public ModsStringAttribute(string str)
        {
            String = str;
        }

        public string String { get; }
    }

    public static class ModsUtilities
    {
        static ModsUtilities()
        {
            ModsStrings = new Dictionary<Mods, string>();

            Type t = typeof(Mods);
            TypeInfo ti = t.GetTypeInfo();
            IEnumerable<Mods> mods = Enum.GetValues(t).Cast<Mods>();

            foreach (Mods mod in mods)
            {
                ModsStrings[mod] = ti.DeclaredMembers.FirstOrDefault(xm => xm.Name == mod.ToString()).GetCustomAttribute<ModsStringAttribute>().String;
            }
        }

        private static Dictionary<Mods, string> ModsStrings { get; }

        public static string ToModString(this Mods mods)
        {
            if (mods == Mods.None)
            {
                return ModsStrings[mods];
            }

            IEnumerable<string> enumerableMods = ModsStrings
                .Where(k => k.Key != Mods.None && (mods & k.Key) == k.Key)
                .Select(k => k.Value);

            return string.Join(OsuApi.ModsSeparator, enumerableMods);
        }
    }
}