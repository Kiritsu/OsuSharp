namespace OsuSharp.Domain
{
    public sealed class Statistics
    {
        public int Count50 { get; internal set; }
        
        public int Count100 { get; internal set; }
        
        public int Count300 { get; internal set; }
        
        public int CountGeki { get; internal set; }
        
        public int CountKatu { get; internal set; }
        
        public int CountMiss { get; internal set; }
    }
}
