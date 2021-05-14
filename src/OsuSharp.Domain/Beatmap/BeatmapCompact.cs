using System;
using OsuSharp.Interfaces;

namespace OsuSharp.Domain
{
    public class BeatmapCompact : IBeatmapCompact
    {
        public double DifficultyRating { get; internal set; }

        public long Id { get; internal set; }

        public GameMode Mode { get; internal set; }

        public RankStatus Status { get; internal set; }

        public TimeSpan Length => _length ??= TimeSpan.FromSeconds(_totalLength);

        private long _totalLength;
        private TimeSpan? _length;

        public string Version { get; internal set; }

        public IBeatmapsetCompact Beatmapset { get; internal set; }

        public string Checksum { get; internal set; }

        public IFailTimes FailTimes { get; internal set; }

        public int? MaxCombo { get; internal set; }
        
        internal BeatmapCompact()
        {
            
        }
    }
}