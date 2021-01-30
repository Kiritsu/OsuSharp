using OsuSharp.Enums;

namespace OsuSharp.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        ///     Returns a string that fits osu! API requirements for that <see cref="BeatmapsetType" />.
        /// </summary>
        /// <param name="type">GameMode to get the string for.</param>
        /// <returns>A api-valid string representation of this <see cref="BeatmapsetType" /></returns>
        public static string ToApiString(this Optional<BeatmapsetType> type)
        {
            if (type.HasValue)
            {
                return type.Value switch
                {
                    BeatmapsetType.MostPlayed => "most_played",
                    BeatmapsetType.Favourite => "favourite",
                    BeatmapsetType.RankedAndApproved => "ranked_and_approved",
                    BeatmapsetType.Unranked => "unranked",
                    BeatmapsetType.Graveyard => "graveyard",
                    _ => ""
                };
            }

            return "";
        }

        /// <summary>
        ///     Returns a string that fits osu! API requirements for that <see cref="GameMode" />.
        /// </summary>
        /// <param name="gameMode">GameMode to get the string for.</param>
        /// <returns>A api-valid string representation of this <see cref="GameMode" /></returns>
        public static string ToApiString(this Optional<GameMode> gameMode)
        {
            if (gameMode.HasValue)
            {
                return gameMode.Value switch
                {
                    GameMode.Osu => "osu",
                    GameMode.Taiko => "taiko",
                    GameMode.Fruits => "fruits",
                    GameMode.Mania => "mania",
                    _ => ""
                };
            }

            return "";
        }
    }
}