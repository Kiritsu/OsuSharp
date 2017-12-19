namespace OsuSharp.Misc
{
    public class Beatmap
    {
        public static string ToString(BeatmapType type)
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
}