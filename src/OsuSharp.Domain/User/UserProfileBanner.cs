using OsuSharp.Interfaces;

namespace OsuSharp.Domain;

public sealed class UserProfileBanner : IUserProfileBanner
{
    public long Id { get; internal set; }

    public long TournamentId { get; internal set; }

    public string Image { get; internal set; } = null!;

    internal UserProfileBanner()
    {
            
    }
}