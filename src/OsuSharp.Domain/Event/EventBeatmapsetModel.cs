using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public sealed class EventBeatmapsetModel : IEventBeatmapsetModel
    {
        public string Title { get; internal set; }

        public string Url { get; internal set; }

        internal EventBeatmapsetModel()
        {
            
        }
    }
}