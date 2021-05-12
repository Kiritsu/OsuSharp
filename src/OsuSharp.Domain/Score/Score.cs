using System;
using System.Collections.Generic;

namespace OsuSharp.Domain
{
    public class Score 
    {
        public long Id { get; internal set; }
        
        public long? BestId { get; internal set; }
        
        public long UserId { get; internal set; }
        
        public double Accuracy { get; internal set; }
        
        public IReadOnlyList<string> Mods { get; internal set; }
        
        public long TotalScore { get; internal set; }
        
        public int MaxCombo { get; internal set; }
        
        public bool Perfect { get; internal set; }
        
        public Statistics Statistics { get; internal set; }
        
        public double? PerformancePoints { get; internal set; }
        
        public string Rank { get; internal set; }
        
        public DateTimeOffset CreatedAt { get; internal set; }
        
        public GameMode Mode { get; internal set; }
        
        public bool? HasReplay { get; internal set; }
        
        public Beatmap Beatmap { get; internal set; }
        
        public Beatmapset Beatmapset { get; internal set; }
        
        public long? CountryRank { get; internal set; }
        
        public long? GlobalRank { get; internal set; }
        
        public Weight Weight { get; internal set; }
        
        public User User { get; internal set; }
        
        // todo: object
        public object Match { get; internal set; }
    }
}
