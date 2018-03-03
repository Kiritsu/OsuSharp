namespace OsuSharp.Misc
{
    public class Approved
    {
        /// <summary>
        /// Returns a <see cref="string"/> of the given beatmap state.
        /// </summary>
        /// <param name="state">State of the beatmap in number.</param>
        /// <returns></returns>
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