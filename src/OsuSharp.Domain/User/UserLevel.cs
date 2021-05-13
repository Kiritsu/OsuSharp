namespace OsuSharp.Domain
{
    public sealed class UserLevel
    {
        public long Current { get; internal set; }

        public long Progress { get; internal set; }

        internal UserLevel()
        {
            
        }
    }
}