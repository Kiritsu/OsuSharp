namespace OsuSharp.BeatmapsEndpoint.Converter
{
    public class Approved
    {
        public static string ToString(string state)
        {
            switch (state)
            {
                case "-2":
                    return "Graveyard";
                case "-1":
                    return "WIP";
                case "0":
                    return "Pending";
                case "1":
                    return "Ranked";
                case "2":
                    return "Approved";
                case "3":
                    return "Qualified";
                case "4":
                    return "Loved";
                default:
                    return "Unknown";
            }
        }
    }
}