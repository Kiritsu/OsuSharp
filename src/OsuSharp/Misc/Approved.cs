using System;

namespace OsuSharp.Misc
{
    public class Approved
    {
        /// <summary>
        /// Returns a <see cref="BeatmapState"/> of the given beatmap state.
        /// </summary>
        /// <param name="currentState">State of the beatmap in number.</param>
        /// <returns></returns>
        public static BeatmapState ToBeatmapState(string currentState)
        {
            return Enum.TryParse(currentState, out BeatmapState state) ? state : BeatmapState.Unknown;
        }
    }
}