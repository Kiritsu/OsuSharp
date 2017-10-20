namespace OsuSharp.Common
{
    public class UserModeConverter
    {
        public static string ConvertToString(OsuMode type)
        {
            switch (type)
            {
                case OsuMode.Standard:
                    return "&m=0";
                case OsuMode.Taiko:
                    return "&m=1";
                case OsuMode.CatchTheBeat:
                    return "&m=2";
                case OsuMode.Mania:
                    return "&m=3";
                default:
                    return "&m=0";
            }
        }
    }
}