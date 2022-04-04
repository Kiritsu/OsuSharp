namespace OsuSharp.Interfaces;

public interface IUserProfileBanner
{
    long Id { get; }
    long TournamentId { get; }
    string Image { get; }
}