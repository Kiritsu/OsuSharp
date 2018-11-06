using OsuSharp.Enums;

namespace OsuSharp.Misc
{
    internal sealed class UserMode
    {
        internal static string ToString(GameMode type)
        {
            switch (type)
            {
                case GameMode.Standard:
                    return "&m=0";
                case GameMode.Taiko:
                    return "&m=1";
                case GameMode.Catch:
                    return "&m=2";
                case GameMode.Mania:
                    return "&m=3";
                default:
                    return "&m=0";
            }
        }
    }
}