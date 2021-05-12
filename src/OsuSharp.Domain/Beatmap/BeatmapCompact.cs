using System;

namespace OsuSharp.Domain
{
    public class BeatmapCompact
    {
        public double DifficultyRating { get; internal set; }
        
        public long Id { get; internal set; }
        
        public GameMode Mode { get; internal set; }
        
        public RankStatus Status { get; internal set; }
        
        public TimeSpan Length => _length ??= TimeSpan.FromSeconds(_totalLength);

        private long _totalLength;
        private TimeSpan? _length;
        
        public string Version { get; internal set; }
        
        public BeatmapsetCompact Beatmapset { get; internal set; }
        
        public string Checksum { get; internal set; }
        
        public FailTimes FailTimes { get; internal set; }
        
        public int? MaxCombo { get; internal set; }
    }
}
