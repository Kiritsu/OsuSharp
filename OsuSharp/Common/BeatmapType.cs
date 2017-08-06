namespace OsuSharp.Common
{
    public class BeatmapsType
    {
        public static string BeatmapTypeConverter(BeatmapType type)
        {
            switch (type)
            {
                case BeatmapType.ByBeatmap:
                    return "&s=";
                case BeatmapType.ByDifficulty:
                    return "&b=";
                case BeatmapType.ByCreator:
                    return "&u=";
                default:
                    return "&s=";
            }
        }
    }
    public enum BeatmapType
    {
        ByBeatmap,
        ByDifficulty, 
        ByCreator
    }
}
