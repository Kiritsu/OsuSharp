using Newtonsoft.Json;

namespace OsuSharp.Entities.Event
{
    public sealed class BeatmapsetDeleteEvent : Event
    {
        [JsonProperty("beatmapset")]
        public EventBeatmapsetModel Beatmapset { get; internal set; }

        internal BeatmapsetDeleteEvent()
        {
            
        }
    }
}