using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class BeatmapCover : IBeatmapCover
    {
        public string Cover { get; internal set; }

        public string Cover2x { get; internal set; }

        public string Card { get; internal set; }

        public string Card2x { get; internal set; }

        public string List { get; internal set; }

        public string List2x { get; internal set; }

        public string SlimCover { get; internal set; }

        public string SlimCover2x { get; internal set; }
        
        internal BeatmapCover()
        {
            
        }
    }
}