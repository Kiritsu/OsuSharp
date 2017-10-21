namespace OsuSharp.Common
{
    public class UserModeConverter
    {
        public static string ConvertToString(GameMode type)
        {
            switch (type)
            {
                case GameMode.Standard:
                    return "&m=0";
                case GameMode.Taiko:
                    return "&m=1";
                case GameMode.CatchTheBeat:
                    return "&m=2";
                case GameMode.Mania:
                    return "&m=3";
                default:
                    return "&m=0";
            }
        }
    }
}