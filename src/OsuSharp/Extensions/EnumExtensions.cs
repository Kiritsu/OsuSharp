using OsuSharp.Domain;

namespace OsuSharp.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        ///     Returns a string that fits osu! API requirements for that <see cref="BeatmapsetType" />.
        /// </summary>
        /// <param name="type">GameMode to get the string for.</param>
        /// <returns>A api-valid string representation of this <see cref="BeatmapsetType" /></returns>
        public static string ToApiString(this ScoreType type)
        {
            return type switch
            {
                ScoreType.Best => "best",
                ScoreType.Firsts => "firsts",
                ScoreType.Recent => "recent",
                _ => ""
            };
        }

        /// <summary>
        ///     Returns a string that fits osu! API requirements for that <see cref="BeatmapsetType" />.
        /// </summary>
        /// <param name="type">GameMode to get the string for.</param>
        /// <returns>A api-valid string representation of this <see cref="BeatmapsetType" /></returns>
        public static string ToApiString(this ScoreType? type)
        {
            return type.HasValue ? ToApiString(type.Value) : "";
        }

        /// <summary>
        ///     Returns a string that fits osu! API requirements for that <see cref="BeatmapsetType" />.
        /// </summary>
        /// <param name="type">GameMode to get the string for.</param>
        /// <returns>A api-valid string representation of this <see cref="BeatmapsetType" /></returns>
        public static string ToApiString(this BeatmapsetType type)
        {
            return type switch
            {
                BeatmapsetType.Favourite => "favourite",
                BeatmapsetType.Graveyard => "graveyard",
                BeatmapsetType.Loved => "loved",
                BeatmapsetType.MostPlayed => "most_played",
                BeatmapsetType.Pending => "pending",
                BeatmapsetType.Ranked => "ranked",
                _ => ""
            };
        }

        /// <summary>
        ///     Returns a string that fits osu! API requirements for that <see cref="BeatmapsetType" />.
        /// </summary>
        /// <param name="type">GameMode to get the string for.</param>
        /// <returns>A api-valid string representation of this <see cref="BeatmapsetType" /></returns>
        public static string ToApiString(this BeatmapsetType? type)
        {
            return type.HasValue ? ToApiString(type.Value) : "";
        }

        /// <summary>
        ///     Returns a string that fits osu! API requirements for that <see cref="GameMode" />.
        /// </summary>
        /// <param name="gameMode">GameMode to get the string for.</param>
        /// <returns>A api-valid string representation of this <see cref="GameMode" /></returns>
        public static string ToApiString(this GameMode gameMode)
        {
            return gameMode switch
            {
                GameMode.Osu => "osu",
                GameMode.Taiko => "taiko",
                GameMode.Fruits => "fruits",
                GameMode.Mania => "mania",
                _ => ""
            };
        }

        /// <summary>
        ///     Returns a string that fits osu! API requirements for that <see cref="GameMode" />.
        /// </summary>
        /// <param name="gameMode">GameMode to get the string for.</param>
        /// <returns>A api-valid string representation of this <see cref="GameMode" /></returns>
        public static string ToApiString(this GameMode? gameMode)
        {
            return gameMode.HasValue ? ToApiString(gameMode.Value) : "";
        }
    }
}