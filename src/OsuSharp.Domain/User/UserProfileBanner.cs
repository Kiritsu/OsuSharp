namespace OsuSharp.Domain
{
    public sealed class UserProfileBanner
    {
        public long Id { get; internal set; }

        public long TournamentId { get; internal set; }

        public string Image { get; internal set; }

        internal UserProfileBanner()
        {
            
        }
    }
}