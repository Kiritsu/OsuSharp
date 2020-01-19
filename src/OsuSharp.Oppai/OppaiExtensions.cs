using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OsuSharp.Oppai
{
    public static class OppaiExtensions
    {
        /// <summary>
        ///     Gets the pp for that beatmap.
        /// </summary>
        public static Task<PerformanceData> GetPPAsync(this Beatmap beatmap, int? combo = null)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId, combo);
        }

        /// <summary>
        ///     Gets the pp for that beatmap with the specified modes.
        /// </summary>
        public static Task<PerformanceData> GetPPAsync(this Beatmap beatmap, Mode mode, int? combo = null)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId, mode, combo);
        }

        /// <summary>
        ///     Gets the pp for that beatmap with the specified accuracy.
        /// </summary>
        public static Task<PerformanceData> GetPPAsync(this Beatmap beatmap, float accuracy, int? combo = null)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId, accuracy, combo);
        }

        /// <summary>
        ///     Gets the pp for that beatmap with the specified modes and accuracy.
        /// </summary>
        public static Task<PerformanceData> GetPPAsync(this Beatmap beatmap, Mode mode, float accuracy, 
            int? combo = null)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId, mode, accuracy, combo);
        }

        /// <summary>
        ///     Gets the pps for that beatmap with the specified accuracies.
        /// </summary>
        public static Task<ReadOnlyDictionary<float, PerformanceData>> GetPPAsync(this Beatmap beatmap, 
            float[] accuracies, int? combo = null)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId, accuracies, combo);
        }

        /// <summary>
        ///     Gets the pps for that beatmap with the specified modes.
        /// </summary>
        public static Task<ReadOnlyDictionary<Mode, PerformanceData>> GetPPAsync(this Beatmap beatmap, 
            Mode[] modes, int? combo = null)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId, modes, combo);
        }

        /// <summary>
        ///     Gets the pps for that beatmap with the specified modes.
        /// </summary>
        public static Task<ReadOnlyDictionary<Mode, PerformanceData>> GetPPAsync(this Beatmap beatmap, 
            Mode[] modes, float accuracy, int? combo = null)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId, modes, accuracy, combo);
        }

        /// <summary>
        ///     Gets the pps for that beatmap with the specified modes.
        /// </summary>
        public static Task<ReadOnlyDictionary<float, PerformanceData>> GetPPAsync(this Beatmap beatmap, 
            Mode modes, float[] accuracies, int? combo = null)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId, modes, accuracies, combo);
        }

        /// <summary>
        ///     Gets the pps for that beatmap with the specified modes and accuracies.
        /// </summary>
        public static Task<ReadOnlyDictionary<Mode, ReadOnlyDictionary<float, PerformanceData>>> GetPPAsync(
            this Beatmap beatmap, Mode[] modes, float[] accuracies, int? combo = null)
        {
            return OppaiClient.GetPPAsync(beatmap.BeatmapId, modes, accuracies, combo);
        }

        /// <summary>
        ///     Gets the pp for that score.
        /// </summary>
        public static Task<PerformanceData> GetPPAsync(this Score score)
        {
            return OppaiClient.GetPPAsync(score.BeatmapId, score.Mods, (float)score.Accuracy, score.MaxCombo);
        }
    }
}
