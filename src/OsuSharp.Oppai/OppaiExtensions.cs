using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OsuSharp.Oppai
{
    public static class OppaiExtensions
    {
        /// <summary>
        ///     Gets the pp for that beatmap.
        /// </summary>
        public static Task<PerformanceData> GetPPAsync(this Beatmap beatmap)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId);
        }

        /// <summary>
        ///     Gets the pp for that beatmap with the specified modes.
        /// </summary>
        public static Task<PerformanceData> GetPPAsync(this Beatmap beatmap, Mode mode)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId, mode);
        }

        /// <summary>
        ///     Gets the pp for that beatmap with the specified accuracy.
        /// </summary>
        public static Task<PerformanceData> GetPPAsync(this Beatmap beatmap, float accuracy)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId, accuracy);
        }

        /// <summary>
        ///     Gets the pp for that beatmap with the specified modes and accuracy.
        /// </summary>
        public static Task<PerformanceData> GetPPAsync(this Beatmap beatmap, Mode mode, float accuracy)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId, mode, accuracy);
        }

        /// <summary>
        ///     Gets the pps for that beatmap with the specified accuracies.
        /// </summary>
        public static Task<ReadOnlyDictionary<float, PerformanceData>> GetPPAsync(this Beatmap beatmap, float[] accuracies)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId, accuracies);
        }

        /// <summary>
        ///     Gets the pps for that beatmap with the specified modes.
        /// </summary>
        public static Task<ReadOnlyDictionary<Mode, PerformanceData>> GetPPAsync(this Beatmap beatmap, Mode[] modes)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId, modes);
        }

        /// <summary>
        ///     Gets the pps for that beatmap with the specified modes.
        /// </summary>
        public static Task<ReadOnlyDictionary<Mode, PerformanceData>> GetPPAsync(this Beatmap beatmap, Mode[] modes, 
            float accuracy)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId, modes, accuracy);
        }

        /// <summary>
        ///     Gets the pps for that beatmap with the specified modes.
        /// </summary>
        public static Task<ReadOnlyDictionary<float, PerformanceData>> GetPPAsync(this Beatmap beatmap, Mode modes, 
            float[] accuracies)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId, modes, accuracies);
        }

        /// <summary>
        ///     Gets the pps for that beatmap with the specified modes and accuracies.
        /// </summary>
        public static Task<ReadOnlyDictionary<Mode, ReadOnlyDictionary<float, PerformanceData>>> GetPPAsync(
            this Beatmap beatmap, Mode[] modes, float[] accuracies)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId, modes, accuracies);
        }

        /// <summary>
        ///     Gets the pp for that score.
        /// </summary>
        public static Task<PerformanceData> GetPPAsync(this Score score)
        {
            return OppaiClient.GetPPAsync(score.BeatmapId, score.Mods, (float)score.Accuracy);
        }
    }
}
